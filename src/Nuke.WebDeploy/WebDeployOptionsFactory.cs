using System.Linq;
using Microsoft.Web.Deployment;

namespace Nuke.WebDeploy
{
    internal static class WebDeployOptionsFactory
    {
        public static DeploymentBaseOptions GetDestinationOptions(WebDeploySettings settings)
        {
            var options = new DeploymentBaseOptions();
            options.AuthenticationType = "basic";
            options.ComputerName = settings.PublishUrl;
            options.UserName = settings.Username;
            options.Password = settings.Password;
            options.RetryAttempts = settings.RetryAttempts ?? default;
            options.RetryInterval = settings.RetryInterval ?? default;
            return options;
        }

        public static DeploymentBaseOptions GetSourceOptions()
        {
            var options = new DeploymentBaseOptions();
            return options;
        }

        public static DeploymentSyncOptions GetSyncOptions(WebDeploySettings settings)
        {
            var options = new DeploymentSyncOptions();
            options.DoNotDelete = settings.EnableDoNotDeleteRule ?? false;
            options.WhatIf = settings.ShowWhatIf ?? false;
            if (settings.EnableAppOfflineRule ?? false)
            {
                var appOfflineRule = GetRuleByName("AppOffline");
                options.Rules.Add(appOfflineRule);
            }

            // TODO MORE RULES
            return options;
        }

        private static DeploymentRule GetRuleByName(string ruleName)
        {
            var availableRules = DeploymentSyncOptions.GetAvailableRules();
            var rule = availableRules.Single(r => string.Equals(r.Name, ruleName, System.StringComparison.InvariantCultureIgnoreCase));
            return rule;
        }
    }
}
