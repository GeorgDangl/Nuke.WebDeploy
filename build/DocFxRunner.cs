using Fallout.Common.Tooling;
using static Fallout.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    private static void RunDocFx(string command, string configuration)
    {
        var executable = NuGetToolPathResolver.GetPackageExecutable("docfx", "tools/net10.0/any/docfx.dll");
        DotNet($"\"{executable}\" {command} \"{configuration}\"");
    }
}
