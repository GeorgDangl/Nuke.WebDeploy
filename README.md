# Nuke.WebDeploy

[![Build Status](https://jenkins.dangl.me/buildStatus/icon?job=Nuke.WebDeploy/dev)](https://jenkins.dangl.me/job/Nuke.WebDeploy/dev)

[![Built with Nuke](http://nuke.build/rounded)](https://www.nuke.build)

[![NuGet](https://img.shields.io/nuget/v/Nuke.WebDeploy.svg)](https://www.nuget.org/packages/Nuke.WebDeploy)
[![MyGet](https://img.shields.io/myget/dangl/v/Nuke.WebDeploy.svg)](https://www.myget.org/feed/dangl/package/nuget/Nuke.WebDeploy)

This plugin provides [Microsoft WebDeploy](https://www.iis.net/downloads/microsoft/web-deploy) functionality
for the [NUKE Build](https://github.com/nuke-build/nuke) system. It relies on the [Microsoft.Web.Deployment](https://www.nuget.org/packages/Microsoft.Web.Deployment/)
NuGet package and therefore only supports builds on Windows. The functionality is quite limited right now, allowing only to deploy
to an IIS or Azure app. Contributions are welcome!

The code is based on [Cake.WebDeploy](https://github.com/SharpeRAD/Cake.WebDeploy).

[Link to documentation](https://docs.dangl-it.com/Projects/Nuke.WebDeploy).

## CI Builds

All builds are available on MyGet:

    https://www.myget.org/F/dangl/api/v2
    https://www.myget.org/F/dangl/api/v3/index.json

## Example

```
Target Deploy => _ => _
    .DependsOn(Publish)
    .Requires(() => WebDeployUsername)
    .Requires(() => WebDeployPassword)
    .Requires(() => WebDeployPublishUrl)
    .Requires(() => WebDeploySiteName)
    .Executes(() =>
    {
        WebDeploy(s => s
                    .SetSourcePath(OutputDirectory)
                    .SetUsername(WebDeployUsername)
                    .SetPassword(WebDeployPassword)
                    .SetEnableAppOfflineRule(true)
                    .SetPublishUrl(WebDeployPublishUrl)
                    .SetSiteName(WebDeploySiteName)
                    .SetWrapAppOffline(true));
    });
```

The `PublishUrl` should include the `site` parameter, e.g.:

    https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname

## License

[This project is available under the MIT license.](LICENSE.md)


## Markdown documentation publishing

`BuildDocumentation` also generates `output/docs-markdown` and `output/docs-markdown.zip`.
Start with `index.md` (the source README), or `toc.md` for all articles and API pages.
Changelog/license text and linked documentation assets are included; the ZIP has no
enclosing directory.

The Markdown renderer converts the existing published DocFX article HTML, including
API signatures, code examples and anchors. This keeps the current documentation
toolchain and public project boundaries intact, without requiring newer DocFX or
migrating the legacy NUKE build. It does not modify documentation sources.

`UploadDocumentation` uploads HTML first, then sends the Markdown ZIP to
`/API/MarkdownDocuments/Upload` with the same API key and exact version. Generation
or upload errors fail publishing; existing branch gates and version-conflict policies
are preserved. `TestMarkdownDocumentation` exercises packaging, links, assets,
failure handling and the multipart contract using only a local HTTP server.
