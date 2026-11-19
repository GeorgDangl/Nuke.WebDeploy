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

The documentation builder uses Fallout.Common 10.5.0 and requires .NET SDK 10.0.100
or later. Product target frameworks are unchanged. Run `build.sh` or `build.ps1`.
`BuildDocumentation` uses DocFX 2.78.5 and packages the configured Markdown,
TOC and resource mappings without modifying source files. API documentation,
where configured, also uses native DocFX Markdown metadata with the same scope.

The Markdown directory is `output/docs-markdown`; its sibling `.zip` has a root
`index.md` and includes mapped documentation assets. `UploadDocumentation` uses
the official Dangl.Fallout.WebDocu 8.0.1 package with `SetMarkdownPackagePath`,
the existing explicit version, credentials, downloadable assets and conflict policy.
No custom HTTP uploader is used. Deployment targets require real credentials;
use `--help` or `--plan` for safe build-graph inspection.

`TestMarkdownDocumentation` checks packaging, navigation and asset preservation locally.

The product remains `Nuke.WebDeploy` with its existing target framework and package identity.
The Fallout builder runs the original NUKE product code generator in an isolated
`build/LegacyCodeGeneration` process so generated product code continues to use the NUKE API.

Azure Key Vault authentication also requires `KeyVaultTenantId` (Jenkins credential
`AzureKeyVaultTenantId`), alongside the existing vault URL, client ID and client secret.
