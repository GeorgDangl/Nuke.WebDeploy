using JetBrains.Annotations;
using Nuke.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nuke.WebDeploy
{
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class WebDeploySettings : ISettingsEntity
    {
        public virtual bool UseDoNotDeleteRule { get; internal set; }
        public virtual string PublishUrl { get; internal set; }
        public virtual string Username { get; internal set; }
        public virtual string Password { get; internal set; }
        public virtual bool EnableDoNotDeleteRule { get; internal set; }
        public virtual bool EnableAppOfflineRule { get; internal set; }
        public virtual bool ShowWhatIf { get; internal set; }
        public virtual string SiteName { get; internal set; }
        public virtual string SourcePath { get; internal set; }
        public virtual string DestinationPath { get; internal set; }
        public virtual IReadOnlyDictionary<string, string> Parameters { get; internal set; }
        public virtual int RetryAttempts { get; internal set; } = 5;
        public virtual int RetryInterval { get; internal set; } = 5000;

        /// <summary>
        /// This is because Azure is still case sensitive when deploying App_Offline.htm via
        /// the WebDeploy rule. An all-lowercase app_offline.htm will be created prior to deployment
        /// and removed afterwards if this option is enabled.
        /// </summary>
        public virtual bool WrapAppOffline { get; internal set; }

        public virtual string AppOfflineHtmlTemplate { get; internal set; } = DefaultAppOfflineProvider.DefaultAppOffline;
    }
}
