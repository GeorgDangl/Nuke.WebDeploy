using System.IO;
using System.Linq;
using Nuke.Common.Tools.DocFx;
using Nuke.Common.Tools.DotNet;
using Nuke.Core;
using Nuke.WebDocu;
using static Nuke.WebDocu.WebDocuTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Core.IO.FileSystemTasks;
using static Nuke.Core.IO.PathConstruction;
using static Nuke.Core.EnvironmentInfo;
using static Nuke.Common.Tools.Xunit.XunitTasks;
using Nuke.Common.Tools.Xunit;
using Nuke.Core.Utilities.Collections;
using static Nuke.Common.Tools.DocFx.DocFxTasks;
using static Nuke.CodeGeneration.CodeGenerator;
using System;
using System.Threading.Tasks;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;
using Nuke.Core.Utilities;
using Nuke.GitHub;
using static Nuke.GitHub.ChangeLogExtensions;
using static Nuke.GitHub.GitHubTasks;
using static Nuke.Common.ChangeLog.ChangelogTasks;

class Build : NukeBuild
{
    // Console application entry. Also defines the default target.
    public static int Main() => Execute<Build>(x => x.Test);

    [GitVersion] readonly GitVersion GitVersion;
    [GitRepository] readonly GitRepository GitRepository;

    [Parameter] readonly string MyGetApiKey;
    [Parameter] readonly string MyGetSource;
    [Parameter] readonly string DocuApiKey;
    [Parameter] readonly string DocuApiEndpoint;
    [Parameter] string GitHubAuthenticationToken;

    string DocFxFile => SolutionDirectory / "docfx.json";
    string ChangeLogFile => RootDirectory / "CHANGELOG.md";

    Target Clean => _ => _
        .Executes(() =>
        {
            DeleteDirectories(GlobDirectories(SourceDirectory, "**/bin", "**/obj"));
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => DefaultDotNetRestore);
        });

    Target Compile => _ => _
        .DependsOn(Generate)
        .Executes(() =>
        {
            DotNetBuild(s => DefaultDotNetBuild
                .SetFileVersion(GitVersion.GetNormalizedFileVersion())
                .SetAssemblyVersion(GitVersion.AssemblySemVer));
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var changeLog = GetCompleteChangeLog(ChangeLogFile)
                .EscapeStringPropertyForMsBuild();
            DotNetPack(s => DefaultDotNetPack
                .SetPackageReleaseNotes(changeLog));
        });

    Target Push => _ => _
        .DependsOn(Pack)
        .Requires(() => MyGetSource)
        .Requires(() => MyGetApiKey)
        .Requires(() => Configuration.EqualsOrdinalIgnoreCase("Release"))
        .Executes(() =>
        {
            GlobFiles(OutputDirectory, "*.nupkg").NotEmpty()
                .Where(x => !x.EndsWith("symbols.nupkg"))
                .ForEach(x => DotNetNuGetPush(s => s
                    .SetTargetPath(x)
                    .SetSource(MyGetSource)
                    .SetApiKey(MyGetApiKey)));
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            void TestXunit()
                => Xunit2(GlobFiles(SolutionDirectory, $"*/bin/{Configuration}/net4*/Nuke.*.Tests.dll").NotEmpty(),
                    s => s.AddResultReport(Xunit2ResultFormat.Xml, OutputDirectory / "tests.xml"));

            TestXunit();
        });

    Target BuildDocFxMetadata => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            if (IsLocalBuild)
            {
                SetVariable("VSINSTALLDIR", @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional");
                SetVariable("VisualStudioVersion", "15.0");
            }
            DocFxMetadata(DocFxFile, s => s.SetLogLevel(DocFxLogLevel.Verbose));
        });

    Target BuildDocumentation => _ => _
        .DependsOn(Clean)
        .DependsOn(BuildDocFxMetadata)
        .Executes(() =>
        {
            // Using README.md as index.md
            File.Copy(SolutionDirectory / "README.md", SolutionDirectory / "index.md");

            DocFxBuild(DocFxFile, s => s
                .ClearXRefMaps()
                .SetLogLevel(DocFxLogLevel.Verbose));

            File.Delete(SolutionDirectory / "index.md");
            Directory.Delete(SolutionDirectory / "api", true);
        });

    Target UploadDocumentation => _ => _
        .DependsOn(Push) // To have a relation between pushed package version and published docs version
        .DependsOn(BuildDocumentation)
        .Requires(() => DocuApiKey)
        .Requires(() => DocuApiEndpoint)
        .Executes(() =>
        {
            WebDocu(s => s.SetDocuApiEndpoint(DocuApiEndpoint)
                .SetDocuApiKey(DocuApiKey)
                .SetSourceDirectory(OutputDirectory / "docs")
                .SetVersion(GitVersion.NuGetVersion));
        });

    Target PublishGitHubRelease => _ => _
        .DependsOn(Pack)
        .Requires(() => GitHubAuthenticationToken)
        .OnlyWhen(() => GitVersion.BranchName.Equals("master") || GitVersion.BranchName.Equals("origin/master"))
        .Executes<Task>(async () =>
        {
            var releaseTag = $"v{GitVersion.MajorMinorPatch}";

            var changeLogSectionEntries = ExtractChangelogSectionNotes(ChangeLogFile);
            var latestChangeLog = changeLogSectionEntries
                .Aggregate((c, n) => c + Environment.NewLine + n);
            var completeChangeLog = $"## {releaseTag}" + Environment.NewLine + latestChangeLog;

            var repositoryInfo = GetGitHubRepositoryInfo(GitRepository);

            await PublishRelease(new GitHubReleaseSettings()
                .SetArtifactPaths(GlobFiles(OutputDirectory, "*.nupkg").NotEmpty().ToArray())
                .SetCommitSha(GitVersion.Sha)
                .SetReleaseNotes(completeChangeLog)
                .SetRepositoryName(repositoryInfo.repositoryName)
                .SetRepositoryOwner(repositoryInfo.gitHubOwner)
                .SetTag(releaseTag)
                .SetToken(GitHubAuthenticationToken)
            );
        });

    Target Generate => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            GenerateCode(
                metadataDirectory: RootDirectory / "src" / "Nuke.WebDeploy" / "MetaData",
                generationBaseDirectory: RootDirectory / "src" / "Nuke.WebDeploy",
                baseNamespace: "Nuke.WebDeploy"
            );
        });
}
