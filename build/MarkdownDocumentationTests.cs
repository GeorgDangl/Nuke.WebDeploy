using System;
using System.IO;
using System.IO.Compression;

internal static class MarkdownDocumentationTests
{
    public static void Run(string directory)
    {
        directory = Path.GetFullPath(directory);
        if (Directory.Exists(directory))
            Directory.Delete(directory, true);
        Directory.CreateDirectory(Path.Combine(directory, "content", "Images", "Nested"));
        Directory.CreateDirectory(Path.Combine(directory, "content", "private"));
        Directory.CreateDirectory(Path.Combine(directory, "output", "docs-markdown", "api"));
        var source = "# Documentation\n\n![diagram](Images/Nested/Diagram.PNG)\n\n[Guide](guide.html#example)\n";
        File.WriteAllText(Path.Combine(directory, "content", "index.md"), source);
        File.WriteAllText(Path.Combine(directory, "content", "guide.md"), "# Guide\n");
        File.WriteAllText(Path.Combine(directory, "content", "toc.yml"), "- name: Guide\n  href: guide.md\n");
        File.WriteAllText(Path.Combine(directory, "content", "private", "secret.md"), "Not published");
        File.WriteAllText(Path.Combine(directory, "content", "Images", "Nested", "Diagram.PNG"), "image");
        File.WriteAllText(Path.Combine(directory, "output", "docs-markdown", "api", "Public.md"),
            "# Public API\n\n```csharp\npublic class Public {}\n```\n");
        var config = Path.Combine(directory, "docfx.json");
        File.WriteAllText(config, """
            {
              "build": {
                "content": [{"src": "content", "files": ["**/*.md", "**/*.yml"], "exclude": ["private/**"]}],
                "resource": [{"src": "content", "files": ["**.png"]}]
              }
            }
            """);
        var output = Path.Combine(directory, "output", "docs-markdown");
        MarkdownDocumentation.Create(config, output);
        using (var zip = ZipFile.OpenRead(output + ".zip"))
        {
            Require(zip.GetEntry("index.md") != null, "Root index is missing.");
            Require(zip.GetEntry("toc.yml") != null, "TOC is missing.");
            Require(zip.GetEntry("Images/Nested/Diagram.PNG") != null, "Nested mixed-case asset is missing.");
            Require(zip.GetEntry("api/Public.md") != null, "Native metadata was removed.");
            Require(zip.GetEntry("private/secret.md") == null, "Excluded content was packaged.");
        }
        Require(File.ReadAllText(Path.Combine(directory, "content", "index.md")) == source, "Source was modified.");
        Require(File.ReadAllText(Path.Combine(output, "index.md")).Contains("guide.md#example"), "HTML link was not rewritten.");
        File.Delete(Path.Combine(directory, "content", "index.md"));
        Directory.Delete(output, true);
        try
        {
            MarkdownDocumentation.Create(config, output);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Native Markdown packaging tests passed.");
            return;
        }
        throw new Exception("Missing index was accepted.");
    }

    private static void Require(bool condition, string message)
    {
        if (!condition)
            throw new Exception(message);
    }
}
