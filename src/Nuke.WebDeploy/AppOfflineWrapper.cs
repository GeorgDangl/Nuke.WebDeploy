using System;
using System.IO;
using Microsoft.Web.Deployment;

namespace Nuke.WebDeploy
{
    public class AppOfflineWrapper : IDisposable
    {
        private readonly WebDeploySettings _settings;
        private string _tempAppOfflinePath;

        public AppOfflineWrapper(WebDeploySettings settings)
        {
            _settings = settings;
            CreateAppOffline();
        }

        public void Dispose()
        {
            RemoveAppOffline();
        }

        private void CreateAppOffline()
        {
            if (!_settings.WrapAppOffline)
            {
                return;
            }

            GenerateTempAppOffline();
            CreateOrDeleteAppOffline(true);
        }

        private void GenerateTempAppOffline()
        {
            _tempAppOfflinePath = Path.GetTempFileName();
            File.WriteAllText(_tempAppOfflinePath, _settings.AppOfflineHtmlTemplate);
        }

        private void RemoveAppOffline()
        {
            if (!_settings.WrapAppOffline)
            {
                return;
            }

            CreateOrDeleteAppOffline(false);
            File.Delete(_tempAppOfflinePath);
        }

        private void CreateOrDeleteAppOffline(bool create = true)
        {
            var sourceOptions = WebDeployOptionsFactory.GetSourceOptions(_settings);
            var destinationOptions = WebDeployOptionsFactory.GetDestinationOptions(_settings);
            destinationOptions.Trace += WebDeployLogger.DestinationOptions_Trace;
            var syncOptions = WebDeployOptionsFactory.GetSyncOptions(_settings);
            if (!create)
            {
                syncOptions.DoNotDelete = false;
            }

            var sourceProvider = DeploymentWellKnownProvider.ContentPath;
            var destinationProvider = DeploymentWellKnownProvider.ContentPath;
            using (var deploymentObject = DeploymentManager.CreateObject(sourceProvider, _tempAppOfflinePath, sourceOptions))
            {
                if (!create)
                {
                    syncOptions.DeleteDestination = true;
                }

                deploymentObject.SyncTo(destinationProvider, _settings.SiteName + "/app_offline.htm", destinationOptions, syncOptions);
            }
        }
    }
}
