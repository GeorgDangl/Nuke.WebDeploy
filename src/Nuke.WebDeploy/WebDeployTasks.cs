using Microsoft.Web.Deployment;
using Nuke.Core.Tooling;

namespace Nuke.WebDeploy
{
    public static class WebDeployTasks
    {
        public static void WebDeploy(Configure<WebDeploySettings> settingsConfigurator)
        {
            var settings = settingsConfigurator(new WebDeploySettings());
            // ReSharper disable once UnusedVariable
            using (var appOfflineWrapper = new AppOfflineWrapper(settings))
            {
                var sourceOptions = WebDeployOptionsFactory.GetSourceOptions();
                var destinationOptions = WebDeployOptionsFactory.GetDestinationOptions(settings);
                destinationOptions.Trace += WebDeployLogger.DestinationOptions_Trace;
                var syncOptions = WebDeployOptionsFactory.GetSyncOptions(settings);
                var sourceProvider = DeploymentWellKnownProvider.IisApp;
                var destinationProvider = DeploymentWellKnownProvider.IisApp;
                using (var deploymentObject = DeploymentManager.CreateObject(sourceProvider, settings.SourcePath, sourceOptions))
                {
                    AppendCustomParameters(settings, deploymentObject);
                    deploymentObject.SyncTo(destinationProvider, settings.SiteName, destinationOptions, syncOptions);
                }
            }
        }

        private static void AppendCustomParameters(WebDeploySettings settings, DeploymentObject deploymentObject)
        {
            if (settings.Parameters != null)
            {
                foreach (var kv in settings.Parameters)
                {
                    if (deploymentObject.SyncParameters.Contains(kv.Key))
                    {
                        deploymentObject.SyncParameters[kv.Key].Value = kv.Value;
                    }
                    else
                    {
                        deploymentObject.SyncParameters.Add(new DeploymentSyncParameter(kv.Key, kv.Key, "", "")
                        {
                            Value = kv.Value
                        });
                    }
                }
            }
        }
    }
}
