using static Nuke.Common.IO.PathConstruction;
using static Nuke.CodeGeneration.CodeGenerator;

internal static class Program
{
    private static void Main(string[] args)
    {
        var root = (AbsolutePath)args[0];
        GenerateCode(
            specificationDirectory: root / "src" / "Nuke.WebDeploy" / "MetaData",
            namespaceProvider: _ => "Nuke.WebDeploy",
            outputFileProvider: _ => root / "src" / "Nuke.WebDeploy" / "WebDeploySettings.Generated.cs");
    }
}
