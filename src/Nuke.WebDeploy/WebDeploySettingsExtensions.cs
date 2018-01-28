using JetBrains.Annotations;
using Nuke.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Nuke.WebDeploy
{
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static class WebDeploySettingsExtensions
    {
        [Pure]
        public static WebDeploySettings SetUseDoNotDeleteRule(this WebDeploySettings toolSettings, bool useDoNotDeleteRule)
            => toolSettings.CloneAndModify(s => s.UseDoNotDeleteRule = useDoNotDeleteRule);

        [Pure]
        public static WebDeploySettings SetPublishUrl(this WebDeploySettings toolSettings, string publishUrl)
            => toolSettings.CloneAndModify(s => s.PublishUrl = publishUrl);

        [Pure]
        public static WebDeploySettings SetUsername(this WebDeploySettings toolSettings, string username)
            => toolSettings.CloneAndModify(s => s.Username = username);

        [Pure]
        public static WebDeploySettings SetPassword(this WebDeploySettings toolSettings, string password)
            => toolSettings.CloneAndModify(s => s.Password = password);

        [Pure]
        public static WebDeploySettings SetEnableDoNotDeleteRule(this WebDeploySettings toolSettings, bool enableDoNotDeleteRule)
            => toolSettings.CloneAndModify(s => s.EnableDoNotDeleteRule = enableDoNotDeleteRule);

        [Pure]
        public static WebDeploySettings SetEnableAppOfflineRule(this WebDeploySettings toolSettings, bool enableAppOfflineRule)
            => toolSettings.CloneAndModify(s => s.EnableAppOfflineRule = enableAppOfflineRule);

        [Pure]
        public static WebDeploySettings SetShowWhatIf(this WebDeploySettings toolSettings, bool showWhatIf)
            => toolSettings.CloneAndModify(s => s.ShowWhatIf = showWhatIf);

        [Pure]
        public static WebDeploySettings SetSiteName(this WebDeploySettings toolSettings, string siteName)
            => toolSettings.CloneAndModify(s => s.SiteName = siteName);

        [Pure]
        public static WebDeploySettings SetSourcePath(this WebDeploySettings toolSettings, string sourcePath)
            => toolSettings.CloneAndModify(s => s.SourcePath = sourcePath);

        [Pure]
        public static WebDeploySettings SetDestinationPath(this WebDeploySettings toolSettings, string destinationPath)
            => toolSettings.CloneAndModify(s => s.DestinationPath = destinationPath);

        [Pure]
        public static WebDeploySettings SetParameters(this WebDeploySettings toolSettings, IDictionary<string, string> parameters)
            => toolSettings.CloneAndModify(s => s.Parameters = new ReadOnlyDictionary<string, string>(parameters));

        [Pure]
        public static WebDeploySettings SetRetryAttempts(this WebDeploySettings toolSettings, int retryAttempts)
            => toolSettings.CloneAndModify(s => s.RetryAttempts = retryAttempts);

        [Pure]
        public static WebDeploySettings SetRetryInterval(this WebDeploySettings toolSettings, int retryInterval)
            => toolSettings.CloneAndModify(s => s.RetryInterval = retryInterval);

        [Pure]
        public static WebDeploySettings SetWrapAppOffline(this WebDeploySettings toolSettings, bool wrapAppOffline)
            => toolSettings.CloneAndModify(s => s.WrapAppOffline = wrapAppOffline);

        [Pure]
        public static WebDeploySettings SetAppOfflineHtmlTemplate(this WebDeploySettings toolSettings, string appOfflineHtmlTemplate)
            => toolSettings.CloneAndModify(s => s.AppOfflineHtmlTemplate = appOfflineHtmlTemplate);

        private static WebDeploySettings CloneAndModify(this WebDeploySettings toolSettings, Action<WebDeploySettings> modifyAction)
        {
            toolSettings = toolSettings.NewInstance();
            modifyAction(toolSettings);
            return toolSettings;
        }
    }
}
