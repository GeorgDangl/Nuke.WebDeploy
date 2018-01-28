using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Nuke.Common.Tools.DocFx;
using Nuke.Common.Tools.DotNet;
using Nuke.Core;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Core.IO.FileSystemTasks;
using static Nuke.Core.IO.PathConstruction;
using static Nuke.Core.EnvironmentInfo;
using static Nuke.Common.Tools.Xunit.XunitTasks;
using Nuke.Common.Tools.Xunit;
using Nuke.Core.Utilities.Collections;
using static Nuke.Common.Tools.DocFx.DocFxTasks;

class Build : NukeBuild
{
    // Console application entry. Also defines the default target.
    public static int Main() => Execute<Build>(x => x.Test);

    [Parameter] readonly string MyGetApiKey;
    [Parameter] readonly string MyGetSource;
    [Parameter] readonly string DocuApiKey;
    [Parameter] readonly string DocuApiEndpoint;

    string DocFxFile => SolutionDirectory / "docfx.json";

    Target Clean => _ => _
        .Executes(() =>
        {
            DeleteDirectories(GlobDirectories(SourceDirectory, "**/bin", "**/obj"));
            EnsureCleanDirectory(OutputDirectory);
            EnsureCleanDirectory(SolutionDirectory / "api");
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => DefaultDotNetRestore);
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => DefaultDotNetBuild);
        });

    Target Publish => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var project = SourceDirectory / "Nuke.WebDeploy" / "Nuke.WebDeploy.csproj";
            DotNetPublish(s => DefaultDotNetPublish
                .SetConfiguration("Release")
                .SetProject(project)
                .SetFramework("net461"));
        });

    Target Push => _ => _
        .DependsOn(Pack)
        .Requires(() => MyGetSource)
        .Requires(() => MyGetApiKey)
        .Executes(() =>
        {
            GlobFiles(OutputDirectory, "*.nupkg").NotEmpty()
                .Where(x => !x.EndsWith("symbols.nupkg"))
                .ForEach(x => DotNetNuGetPush(s => s
                    .SetTargetPath(x)
                    .SetSource(MyGetSource)
                    .SetApiKey(MyGetApiKey)));
        });

    Target Pack => _ => _
        .DependsOn(Compile, Publish)
        .Executes(() =>
        {
            DotNetPack(s => DefaultDotNetPack);
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
        .Executes(() => DocFxBuild(DocFxFile, s => s
            .ClearXRefMaps()
            .SetLogLevel(DocFxLogLevel.Verbose)));

    Target UploadDocumentation => _ => _
        .DependsOn(Push) // To have a relation between pushed package version and published docs version
        .DependsOn(BuildDocumentation)
        .Requires(() => DocuApiKey)
        .Requires(() => DocuApiEndpoint)
        .Executes(() =>
        {
            // Get Version
            var packageVersion = GlobFiles(OutputDirectory, "*.nupkg").NotEmpty()
                .Where(x => !x.EndsWith("symbols.nupkg"))
                .Select(Path.GetFileName)
                .Select(GetVersionFromPackageFilename)
                .First();

            // Create zip package
            if (File.Exists(OutputDirectory / "docs.zip"))
            {
                File.Delete(OutputDirectory / "docs.zip");
            }

            ZipFile.CreateFromDirectory(OutputDirectory / "docs", OutputDirectory / "docs.zip");

            // Upload package to docs
            UploadToDanglDocu(packageVersion).ConfigureAwait(false).GetAwaiter().GetResult();
        });

    static string GetVersionFromPackageFilename(string packageFilename)
    {
        return packageFilename.Replace(".nupkg", string.Empty)
            .Replace("Nuke.WebDeploy.", string.Empty);
    }

    async Task UploadToDanglDocu(string version)
    {
        using (var docsStream = File.OpenRead(OutputDirectory / "docs.zip"))
        {
            var request = new HttpRequestMessage(HttpMethod.Post, DocuApiEndpoint);
            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(DocuApiKey), "ApiKey");
            requestContent.Add(new StringContent(version), "Version");
            requestContent.Add(new StreamContent(docsStream), "ProjectPackage", "docs.zip");
            request.Content = requestContent;
            var response = await new HttpClient().SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Upload failed");
            }
        }
    }
}
