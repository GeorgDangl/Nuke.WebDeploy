using System;
using Nuke.Common.Tooling;

namespace Nuke.WebDeploy
{
    public partial class WebDeploySettings
    {
        private static string DefaultAppOfflineHtmlTemplate { get; } = BaseAppOfflineTemplateProvider.DefaultAppOfflineTemplate;

        /// <summary>
        /// This property is missing in the auto generated code and I have no idea what this should return by default.
        /// </summary>
        public override Action<OutputType, string> ProcessCustomLogger { get; }
    }
}
