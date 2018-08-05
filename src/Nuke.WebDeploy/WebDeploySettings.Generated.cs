// Copyright Matthias Koch, Sebastian Karasek 2018.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

// Generated with Nuke.CodeGeneration, Version: 0.6.0 [CommitSha: 5a428f0d].

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Nuke.WebDeploy
{
    #region WebDeploySettings
    /// <summary><p>Used within <see cref="WebDeployTasks"/>.</p></summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class WebDeploySettings : ToolSettings
    {
        /// <summary><p>The PublishUrl should include the site parameter, e.g.: https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname</p></summary>
        public virtual string PublishUrl { get; internal set; }
        /// <summary><p>The username used for the deployment via http basic authentication</p></summary>
        public virtual string Username { get; internal set; }
        /// <summary><p>The password used for the deployment via http basic authentication</p></summary>
        public virtual string Password { get; internal set; }
        /// <summary><p>Toggles enabling of the DoNotDelete rule</p></summary>
        public virtual bool? EnableDoNotDeleteRule { get; internal set; }
        /// <summary><p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p></summary>
        public virtual bool? EnableAppOfflineRule { get; internal set; }
        /// <summary><p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p></summary>
        public virtual bool? ShowWhatIf { get; internal set; }
        /// <summary><p>The name of the site for web deploy.</p></summary>
        public virtual string SiteName { get; internal set; }
        /// <summary><p>The source folder that should be synced to, e.g. the local publish folder.</p></summary>
        public virtual string SourcePath { get; internal set; }
        /// <summary><p>Count of retry attempts to perform before giving up. This is useful when sites take a few moments to spin down and release locked files.</p></summary>
        public virtual int? RetryAttempts { get; internal set; } = 5;
        /// <summary><p>Delay in milliseconds between retry attempts.</p></summary>
        public virtual int? RetryInterval { get; internal set; } = 5000;
        /// <summary><p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p></summary>
        public virtual bool? WrapAppOffline { get; internal set; }
        /// <summary><p>A html string that is used for the content of the App_Offline.htm file. If not specified, a default message is shown.</p></summary>
        public virtual string AppOfflineHtmlTemplate { get; internal set; } = DefaultAppOfflineHtmlTemplate;
        /// <summary><p>Additional web deploy sync parameters.</p></summary>
        public virtual IReadOnlyDictionary<string, string> Parameters => ParametersInternal.AsReadOnly();
        internal Dictionary<string,string> ParametersInternal { get; set; } = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
        protected override void AssertValid()
        {
            base.AssertValid();
            ControlFlow.Assert(PublishUrl != null, "PublishUrl != null");
            ControlFlow.Assert(Username != null, "Username != null");
            ControlFlow.Assert(Password != null, "Password != null");
            ControlFlow.Assert(SiteName != null, "SiteName != null");
            ControlFlow.Assert(Directory.Exists(SourcePath), "Directory.Exists(SourcePath)");
        }
    }
    #endregion
    #region WebDeploySettingsExtensions
    /// <summary><p>Used within <see cref="WebDeployTasks"/>.</p></summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class WebDeploySettingsExtensions
    {
        #region PublishUrl
        /// <summary><p><em>Sets <see cref="WebDeploySettings.PublishUrl"/>.</em></p><p>The PublishUrl should include the site parameter, e.g.: https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname</p></summary>
        [Pure]
        public static WebDeploySettings SetPublishUrl(this WebDeploySettings toolSettings, string publishUrl)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.PublishUrl = publishUrl;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.PublishUrl"/>.</em></p><p>The PublishUrl should include the site parameter, e.g.: https://appname.scm.azurewebsites.net:443/msdeploy.axd?site=appname</p></summary>
        [Pure]
        public static WebDeploySettings ResetPublishUrl(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.PublishUrl = null;
            return toolSettings;
        }
        #endregion
        #region Username
        /// <summary><p><em>Sets <see cref="WebDeploySettings.Username"/>.</em></p><p>The username used for the deployment via http basic authentication</p></summary>
        [Pure]
        public static WebDeploySettings SetUsername(this WebDeploySettings toolSettings, string username)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Username = username;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.Username"/>.</em></p><p>The username used for the deployment via http basic authentication</p></summary>
        [Pure]
        public static WebDeploySettings ResetUsername(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Username = null;
            return toolSettings;
        }
        #endregion
        #region Password
        /// <summary><p><em>Sets <see cref="WebDeploySettings.Password"/>.</em></p><p>The password used for the deployment via http basic authentication</p></summary>
        [Pure]
        public static WebDeploySettings SetPassword(this WebDeploySettings toolSettings, string password)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Password = password;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.Password"/>.</em></p><p>The password used for the deployment via http basic authentication</p></summary>
        [Pure]
        public static WebDeploySettings ResetPassword(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Password = null;
            return toolSettings;
        }
        #endregion
        #region EnableDoNotDeleteRule
        /// <summary><p><em>Sets <see cref="WebDeploySettings.EnableDoNotDeleteRule"/>.</em></p><p>Toggles enabling of the DoNotDelete rule</p></summary>
        [Pure]
        public static WebDeploySettings SetEnableDoNotDeleteRule(this WebDeploySettings toolSettings, bool? enableDoNotDeleteRule)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = enableDoNotDeleteRule;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.EnableDoNotDeleteRule"/>.</em></p><p>Toggles enabling of the DoNotDelete rule</p></summary>
        [Pure]
        public static WebDeploySettings ResetEnableDoNotDeleteRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="WebDeploySettings.EnableDoNotDeleteRule"/>.</em></p><p>Toggles enabling of the DoNotDelete rule</p></summary>
        [Pure]
        public static WebDeploySettings EnableEnableDoNotDeleteRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="WebDeploySettings.EnableDoNotDeleteRule"/>.</em></p><p>Toggles enabling of the DoNotDelete rule</p></summary>
        [Pure]
        public static WebDeploySettings DisableEnableDoNotDeleteRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="WebDeploySettings.EnableDoNotDeleteRule"/>.</em></p><p>Toggles enabling of the DoNotDelete rule</p></summary>
        [Pure]
        public static WebDeploySettings ToggleEnableDoNotDeleteRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableDoNotDeleteRule = !toolSettings.EnableDoNotDeleteRule;
            return toolSettings;
        }
        #endregion
        #region EnableAppOfflineRule
        /// <summary><p><em>Sets <see cref="WebDeploySettings.EnableAppOfflineRule"/>.</em></p><p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p></summary>
        [Pure]
        public static WebDeploySettings SetEnableAppOfflineRule(this WebDeploySettings toolSettings, bool? enableAppOfflineRule)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = enableAppOfflineRule;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.EnableAppOfflineRule"/>.</em></p><p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p></summary>
        [Pure]
        public static WebDeploySettings ResetEnableAppOfflineRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="WebDeploySettings.EnableAppOfflineRule"/>.</em></p><p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p></summary>
        [Pure]
        public static WebDeploySettings EnableEnableAppOfflineRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="WebDeploySettings.EnableAppOfflineRule"/>.</em></p><p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p></summary>
        [Pure]
        public static WebDeploySettings DisableEnableAppOfflineRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="WebDeploySettings.EnableAppOfflineRule"/>.</em></p><p>Toggles enabling of the AppOfflineRule. If enabled, WebDeploy will create a file called App_Offline.htm before copying the content and remove it afterwards. This makes IIS suspend the site during the deployment process and frees locked files.</p></summary>
        [Pure]
        public static WebDeploySettings ToggleEnableAppOfflineRule(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.EnableAppOfflineRule = !toolSettings.EnableAppOfflineRule;
            return toolSettings;
        }
        #endregion
        #region ShowWhatIf
        /// <summary><p><em>Sets <see cref="WebDeploySettings.ShowWhatIf"/>.</em></p><p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p></summary>
        [Pure]
        public static WebDeploySettings SetShowWhatIf(this WebDeploySettings toolSettings, bool? showWhatIf)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = showWhatIf;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.ShowWhatIf"/>.</em></p><p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p></summary>
        [Pure]
        public static WebDeploySettings ResetShowWhatIf(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="WebDeploySettings.ShowWhatIf"/>.</em></p><p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p></summary>
        [Pure]
        public static WebDeploySettings EnableShowWhatIf(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="WebDeploySettings.ShowWhatIf"/>.</em></p><p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p></summary>
        [Pure]
        public static WebDeploySettings DisableShowWhatIf(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="WebDeploySettings.ShowWhatIf"/>.</em></p><p>If enabled, no actual operations are performed. The output is still printed to the console to display what would have happened.</p></summary>
        [Pure]
        public static WebDeploySettings ToggleShowWhatIf(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ShowWhatIf = !toolSettings.ShowWhatIf;
            return toolSettings;
        }
        #endregion
        #region SiteName
        /// <summary><p><em>Sets <see cref="WebDeploySettings.SiteName"/>.</em></p><p>The name of the site for web deploy.</p></summary>
        [Pure]
        public static WebDeploySettings SetSiteName(this WebDeploySettings toolSettings, string siteName)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SiteName = siteName;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.SiteName"/>.</em></p><p>The name of the site for web deploy.</p></summary>
        [Pure]
        public static WebDeploySettings ResetSiteName(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SiteName = null;
            return toolSettings;
        }
        #endregion
        #region SourcePath
        /// <summary><p><em>Sets <see cref="WebDeploySettings.SourcePath"/>.</em></p><p>The source folder that should be synced to, e.g. the local publish folder.</p></summary>
        [Pure]
        public static WebDeploySettings SetSourcePath(this WebDeploySettings toolSettings, string sourcePath)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SourcePath = sourcePath;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.SourcePath"/>.</em></p><p>The source folder that should be synced to, e.g. the local publish folder.</p></summary>
        [Pure]
        public static WebDeploySettings ResetSourcePath(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.SourcePath = null;
            return toolSettings;
        }
        #endregion
        #region RetryAttempts
        /// <summary><p><em>Sets <see cref="WebDeploySettings.RetryAttempts"/>.</em></p><p>Count of retry attempts to perform before giving up. This is useful when sites take a few moments to spin down and release locked files.</p></summary>
        [Pure]
        public static WebDeploySettings SetRetryAttempts(this WebDeploySettings toolSettings, int? retryAttempts)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryAttempts = retryAttempts;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.RetryAttempts"/>.</em></p><p>Count of retry attempts to perform before giving up. This is useful when sites take a few moments to spin down and release locked files.</p></summary>
        [Pure]
        public static WebDeploySettings ResetRetryAttempts(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryAttempts = null;
            return toolSettings;
        }
        #endregion
        #region RetryInterval
        /// <summary><p><em>Sets <see cref="WebDeploySettings.RetryInterval"/>.</em></p><p>Delay in milliseconds between retry attempts.</p></summary>
        [Pure]
        public static WebDeploySettings SetRetryInterval(this WebDeploySettings toolSettings, int? retryInterval)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryInterval = retryInterval;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.RetryInterval"/>.</em></p><p>Delay in milliseconds between retry attempts.</p></summary>
        [Pure]
        public static WebDeploySettings ResetRetryInterval(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RetryInterval = null;
            return toolSettings;
        }
        #endregion
        #region WrapAppOffline
        /// <summary><p><em>Sets <see cref="WebDeploySettings.WrapAppOffline"/>.</em></p><p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p></summary>
        [Pure]
        public static WebDeploySettings SetWrapAppOffline(this WebDeploySettings toolSettings, bool? wrapAppOffline)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = wrapAppOffline;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.WrapAppOffline"/>.</em></p><p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p></summary>
        [Pure]
        public static WebDeploySettings ResetWrapAppOffline(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="WebDeploySettings.WrapAppOffline"/>.</em></p><p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p></summary>
        [Pure]
        public static WebDeploySettings EnableWrapAppOffline(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="WebDeploySettings.WrapAppOffline"/>.</em></p><p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p></summary>
        [Pure]
        public static WebDeploySettings DisableWrapAppOffline(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="WebDeploySettings.WrapAppOffline"/>.</em></p><p>Azure Web Apps in IIS are case sensitive and only accept all-lowercase app_offline.htm files. If this is toggled, an appropriately named file is created on the destination before deployment and deleted afterwards.</p></summary>
        [Pure]
        public static WebDeploySettings ToggleWrapAppOffline(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WrapAppOffline = !toolSettings.WrapAppOffline;
            return toolSettings;
        }
        #endregion
        #region AppOfflineHtmlTemplate
        /// <summary><p><em>Sets <see cref="WebDeploySettings.AppOfflineHtmlTemplate"/>.</em></p><p>A html string that is used for the content of the App_Offline.htm file. If not specified, a default message is shown.</p></summary>
        [Pure]
        public static WebDeploySettings SetAppOfflineHtmlTemplate(this WebDeploySettings toolSettings, string appOfflineHtmlTemplate)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.AppOfflineHtmlTemplate = appOfflineHtmlTemplate;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="WebDeploySettings.AppOfflineHtmlTemplate"/>.</em></p><p>A html string that is used for the content of the App_Offline.htm file. If not specified, a default message is shown.</p></summary>
        [Pure]
        public static WebDeploySettings ResetAppOfflineHtmlTemplate(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.AppOfflineHtmlTemplate = null;
            return toolSettings;
        }
        #endregion
        #region Parameters
        /// <summary><p><em>Sets <see cref="WebDeploySettings.Parameters"/> to a new dictionary.</em></p><p>Additional web deploy sync parameters.</p></summary>
        [Pure]
        public static WebDeploySettings SetParameters(this WebDeploySettings toolSettings, IDictionary<string, string> parameters)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal = parameters.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);
            return toolSettings;
        }
        /// <summary><p><em>Clears <see cref="WebDeploySettings.Parameters"/>.</em></p><p>Additional web deploy sync parameters.</p></summary>
        [Pure]
        public static WebDeploySettings ClearParameters(this WebDeploySettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal.Clear();
            return toolSettings;
        }
        /// <summary><p><em>Adds a new key-value-pair <see cref="WebDeploySettings.Parameters"/>.</em></p><p>Additional web deploy sync parameters.</p></summary>
        [Pure]
        public static WebDeploySettings AddParameter(this WebDeploySettings toolSettings, string parameterKey, string parameterValue)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal.Add(parameterKey, parameterValue);
            return toolSettings;
        }
        /// <summary><p><em>Removes a key-value-pair from <see cref="WebDeploySettings.Parameters"/>.</em></p><p>Additional web deploy sync parameters.</p></summary>
        [Pure]
        public static WebDeploySettings RemoveParameter(this WebDeploySettings toolSettings, string parameterKey)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal.Remove(parameterKey);
            return toolSettings;
        }
        /// <summary><p><em>Sets a key-value-pair in <see cref="WebDeploySettings.Parameters"/>.</em></p><p>Additional web deploy sync parameters.</p></summary>
        [Pure]
        public static WebDeploySettings SetParameter(this WebDeploySettings toolSettings, string parameterKey, string parameterValue)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ParametersInternal[parameterKey] = parameterValue;
            return toolSettings;
        }
        #endregion
    }
    #endregion
}
