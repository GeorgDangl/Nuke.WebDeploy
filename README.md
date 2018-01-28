# Nuke.WebDeploy

[![Build Status](https://jenkins.dangl.me/buildStatus/icon?job=Nuke.WebDeploy.Tests)](https://jenkins.dangl.me/job/Nuke.WebDeploy.Tests/)
[![MyGet](https://img.shields.io/myget/dangl/v/Nuke.WebDeploy.svg)]()

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
        WebDeploy(s =>
        {
            return s.SetSourcePath(OutputDirectory)
                    .SetUsername(WebDeployUsername)
                    .SetPassword(WebDeployPassword)
                    .SetEnableAppOfflineRule(true)
                    .SetPublishUrl(WebDeployPublishUrl)
                    .SetSiteName(WebDeploySiteName)
                    .SetWrapAppOffline(true);
        });
    });
```

The `PublishUrl` should include the `site` parameter, e.g.:

    https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname

## License

[This project is available under the MIT license.](LICENSE.md)
