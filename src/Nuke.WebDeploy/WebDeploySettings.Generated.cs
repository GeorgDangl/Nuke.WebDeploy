
using JetBrains.Annotations;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Nuke.WebDeploy
{
    #region WebDeploySettings
    /// <summary>
    ///   Used within <see cref="WebDeployTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class WebDeploySettings : ToolSettings
    {
        /// <summary>
        ///   The PublishUrl should include the site parameter, e.g.: https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname
        /// </summary>
        public virtual string PublishUrl { get; internal set; }
        /// <summary>
        ///   The username used for the deployment via http basic authentication
        /// </summary>
        public virtual string Username { get; internal set; }
        /// <summary>
        ///   The password used for the deployment via http basic authentication
        /// </summary>
        public virtual string Password { get; internal set; }
        /// <summary>
        ///   Toggles enabling of the DoNotDelete rule
        /// </summary>
        public virtual bool? EnableDoNotDeleteRule { get; internal set; }
        /// <summary>
        ///   Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.
        /// </summary>
        public virtual bool? EnableAppOfflineRule { get; internal set; }
        /// <summary>
        ///   If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.
        /// </summary>
        public virtual bool? ShowWhatIf { get; internal set; }
        /// <summary>
        ///   The name of the site for web deploy.
        /// </summary>
        public virtual string SiteName { get; internal set; }
        /// <summary>
        ///   The source folder that should be synced to, e.g. the local publish folder.
        /// </summary>
        public virtual string SourcePath { get; internal set; }
        /// <summary>
        ///   Count of retry attempts to perform before giving up. This is useful when sites take a few moments to spin down and release locked files.
        /// </summary>
        public virtual int? RetryAttempts { get; internal set; } = 5;
        /// <summary>
        ///   Delay in milliseconds between retry attempts.
        /// </summary>
        public virtual int? RetryInterval { get; internal set; } = 5000;
        /// <summary>
        ///   Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.
        /// </summary>
        public virtual bool? WrapAppOffline { get; internal set; }
        /// <summary>
        ///   A html string that is used for the content of the App_Offline.htm file. If not specified, a default message is shown.
        /// </summary>
        public virtual string AppOfflineHtmlTemplate { get; internal set; } = DefaultAppOfflineHtmlTemplate;
        /// <summary>
        ///   Additional web deploy sync parameters.
        /// </summary>
        public virtual IReadOnlyDictionary<string, string> Parameters => ParametersInternal.AsReadOnly();
        internal Dictionary<string,string> ParametersInternal { get; set; } = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
    }
    #endregion
    #region WebDeploySettingsExtensions
    /// <summary>
    ///   Used within <see cref="WebDeployTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class WebDeploySettingsExtensions
    {
        #region PublishUrl
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.PublishUrl"/></em></p>
        ///   <p>The PublishUrl should include the site parameter, e.g.: https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname</p>
        /// </summary>
        [Pure]
        public static T SetPublishUrl<T>(this T toolSettings, string publishUrl) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.PublishUrl = publishUrl;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.PublishUrl"/></em></p>
        ///   <p>The PublishUrl should include the site parameter, e.g.: https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname</p>
        /// </summary>
        [Pure]
        public static T ResetPublishUrl<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.PublishUrl = null;
            return toolSettings;
        }
        #endregion
        #region Username
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.Username"/></em></p>
        ///   <p>The username used for the deployment via http basic authentication</p>
        /// </summary>
        [Pure]
        public static T SetUsername<T>(this T toolSettings, string username) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Username = username;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.Username"/></em></p>
        ///   <p>The username used for the deployment via http basic authentication</p>
        /// </summary>
        [Pure]
        public static T ResetUsername<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Username = null;
            return toolSettings;
        }
        #endregion
        #region Password
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.Password"/></em></p>
        ///   <p>The password used for the deployment via http basic authentication</p>
        /// </summary>
        [Pure]
        public static T SetPassword<T>(this T toolSettings, string password) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Password = password;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.Password"/></em></p>
        ///   <p>The password used for the deployment via http basic authentication</p>
        /// </summary>
        [Pure]
        public static T ResetPassword<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Password = null;
            return toolSettings;
        }
        #endregion
        #region EnableDoNotDeleteRule
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.EnableDoNotDeleteRule"/></em></p>
        ///   <p>Toggles enabling of the DoNotDelete rule</p>
        /// </summary>
        [Pure]
        public static T SetEnableDoNotDeleteRule<T>(this T toolSettings, bool? enableDoNotDeleteRule) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = enableDoNotDeleteRule;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.EnableDoNotDeleteRule"/></em></p>
        ///   <p>Toggles enabling of the DoNotDelete rule</p>
        /// </summary>
        [Pure]
        public static T ResetEnableDoNotDeleteRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="WebDeploySettings.EnableDoNotDeleteRule"/></em></p>
        ///   <p>Toggles enabling of the DoNotDelete rule</p>
        /// </summary>
        [Pure]
        public static T EnableEnableDoNotDeleteRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="WebDeploySettings.EnableDoNotDeleteRule"/></em></p>
        ///   <p>Toggles enabling of the DoNotDelete rule</p>
        /// </summary>
        [Pure]
        public static T DisableEnableDoNotDeleteRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="WebDeploySettings.EnableDoNotDeleteRule"/></em></p>
        ///   <p>Toggles enabling of the DoNotDelete rule</p>
        /// </summary>
        [Pure]
        public static T ToggleEnableDoNotDeleteRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = !toolSettings.EnableDoNotDeleteRule;
            return toolSettings;
        }
        #endregion
        #region EnableAppOfflineRule
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.EnableAppOfflineRule"/></em></p>
        ///   <p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p>
        /// </summary>
        [Pure]
        public static T SetEnableAppOfflineRule<T>(this T toolSettings, bool? enableAppOfflineRule) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = enableAppOfflineRule;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.EnableAppOfflineRule"/></em></p>
        ///   <p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p>
        /// </summary>
        [Pure]
        public static T ResetEnableAppOfflineRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="WebDeploySettings.EnableAppOfflineRule"/></em></p>
        ///   <p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p>
        /// </summary>
        [Pure]
        public static T EnableEnableAppOfflineRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="WebDeploySettings.EnableAppOfflineRule"/></em></p>
        ///   <p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p>
        /// </summary>
        [Pure]
        public static T DisableEnableAppOfflineRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="WebDeploySettings.EnableAppOfflineRule"/></em></p>
        ///   <p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p>
        /// </summary>
        [Pure]
        public static T ToggleEnableAppOfflineRule<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = !toolSettings.EnableAppOfflineRule;
            return toolSettings;
        }
        #endregion
        #region ShowWhatIf
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.ShowWhatIf"/></em></p>
        ///   <p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p>
        /// </summary>
        [Pure]
        public static T SetShowWhatIf<T>(this T toolSettings, bool? showWhatIf) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = showWhatIf;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.ShowWhatIf"/></em></p>
        ///   <p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p>
        /// </summary>
        [Pure]
        public static T ResetShowWhatIf<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="WebDeploySettings.ShowWhatIf"/></em></p>
        ///   <p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p>
        /// </summary>
        [Pure]
        public static T EnableShowWhatIf<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="WebDeploySettings.ShowWhatIf"/></em></p>
        ///   <p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p>
        /// </summary>
        [Pure]
        public static T DisableShowWhatIf<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="WebDeploySettings.ShowWhatIf"/></em></p>
        ///   <p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p>
        /// </summary>
        [Pure]
        public static T ToggleShowWhatIf<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = !toolSettings.ShowWhatIf;
            return toolSettings;
        }
        #endregion
        #region SiteName
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.SiteName"/></em></p>
        ///   <p>The name of the site for web deploy.</p>
        /// </summary>
        [Pure]
        public static T SetSiteName<T>(this T toolSettings, string siteName) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SiteName = siteName;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.SiteName"/></em></p>
        ///   <p>The name of the site for web deploy.</p>
        /// </summary>
        [Pure]
        public static T ResetSiteName<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SiteName = null;
            return toolSettings;
        }
        #endregion
        #region SourcePath
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.SourcePath"/></em></p>
        ///   <p>The source folder that should be synced to, e.g. the local publish folder.</p>
        /// </summary>
        [Pure]
        public static T SetSourcePath<T>(this T toolSettings, string sourcePath) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SourcePath = sourcePath;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.SourcePath"/></em></p>
        ///   <p>The source folder that should be synced to, e.g. the local publish folder.</p>
        /// </summary>
        [Pure]
        public static T ResetSourcePath<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SourcePath = null;
            return toolSettings;
        }
        #endregion
        #region RetryAttempts
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.RetryAttempts"/></em></p>
        ///   <p>Count of retry attempts to perform before giving up. This is useful when sites take a few moments to spin down and release locked files.</p>
        /// </summary>
        [Pure]
        public static T SetRetryAttempts<T>(this T toolSettings, int? retryAttempts) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryAttempts = retryAttempts;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.RetryAttempts"/></em></p>
        ///   <p>Count of retry attempts to perform before giving up. This is useful when sites take a few moments to spin down and release locked files.</p>
        /// </summary>
        [Pure]
        public static T ResetRetryAttempts<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryAttempts = null;
            return toolSettings;
        }
        #endregion
        #region RetryInterval
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.RetryInterval"/></em></p>
        ///   <p>Delay in milliseconds between retry attempts.</p>
        /// </summary>
        [Pure]
        public static T SetRetryInterval<T>(this T toolSettings, int? retryInterval) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryInterval = retryInterval;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.RetryInterval"/></em></p>
        ///   <p>Delay in milliseconds between retry attempts.</p>
        /// </summary>
        [Pure]
        public static T ResetRetryInterval<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryInterval = null;
            return toolSettings;
        }
        #endregion
        #region WrapAppOffline
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.WrapAppOffline"/></em></p>
        ///   <p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p>
        /// </summary>
        [Pure]
        public static T SetWrapAppOffline<T>(this T toolSettings, bool? wrapAppOffline) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = wrapAppOffline;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.WrapAppOffline"/></em></p>
        ///   <p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p>
        /// </summary>
        [Pure]
        public static T ResetWrapAppOffline<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="WebDeploySettings.WrapAppOffline"/></em></p>
        ///   <p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p>
        /// </summary>
        [Pure]
        public static T EnableWrapAppOffline<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="WebDeploySettings.WrapAppOffline"/></em></p>
        ///   <p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p>
        /// </summary>
        [Pure]
        public static T DisableWrapAppOffline<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="WebDeploySettings.WrapAppOffline"/></em></p>
        ///   <p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p>
        /// </summary>
        [Pure]
        public static T ToggleWrapAppOffline<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = !toolSettings.WrapAppOffline;
            return toolSettings;
        }
        #endregion
        #region AppOfflineHtmlTemplate
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.AppOfflineHtmlTemplate"/></em></p>
        ///   <p>A html string that is used for the content of the App_Offline.htm file. If not specified, a default message is shown.</p>
        /// </summary>
        [Pure]
        public static T SetAppOfflineHtmlTemplate<T>(this T toolSettings, string appOfflineHtmlTemplate) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.AppOfflineHtmlTemplate = appOfflineHtmlTemplate;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="WebDeploySettings.AppOfflineHtmlTemplate"/></em></p>
        ///   <p>A html string that is used for the content of the App_Offline.htm file. If not specified, a default message is shown.</p>
        /// </summary>
        [Pure]
        public static T ResetAppOfflineHtmlTemplate<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.AppOfflineHtmlTemplate = null;
            return toolSettings;
        }
        #endregion
        #region Parameters
        /// <summary>
        ///   <p><em>Sets <see cref="WebDeploySettings.Parameters"/> to a new dictionary</em></p>
        ///   <p>Additional web deploy sync parameters.</p>
        /// </summary>
        [Pure]
        public static T SetParameters<T>(this T toolSettings, IDictionary<string, string> parameters) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal = parameters.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Clears <see cref="WebDeploySettings.Parameters"/></em></p>
        ///   <p>Additional web deploy sync parameters.</p>
        /// </summary>
        [Pure]
        public static T ClearParameters<T>(this T toolSettings) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal.Clear();
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Adds a new key-value-pair <see cref="WebDeploySettings.Parameters"/></em></p>
        ///   <p>Additional web deploy sync parameters.</p>
        /// </summary>
        [Pure]
        public static T AddParameter<T>(this T toolSettings, string parameterKey, string parameterValue) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal.Add(parameterKey, parameterValue);
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Removes a key-value-pair from <see cref="WebDeploySettings.Parameters"/></em></p>
        ///   <p>Additional web deploy sync parameters.</p>
        /// </summary>
        [Pure]
        public static T RemoveParameter<T>(this T toolSettings, string parameterKey) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal.Remove(parameterKey);
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Sets a key-value-pair in <see cref="WebDeploySettings.Parameters"/></em></p>
        ///   <p>Additional web deploy sync parameters.</p>
        /// </summary>
        [Pure]
        public static T SetParameter<T>(this T toolSettings, string parameterKey, string parameterValue) where T : WebDeploySettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal[parameterKey] = parameterValue;
            return toolSettings;
        }
        #endregion
    }
    #endregion
}
