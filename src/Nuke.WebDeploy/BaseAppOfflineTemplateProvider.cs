using System.IO;

namespace Nuke.WebDeploy
{
    internal static class BaseAppOfflineTemplateProvider
    {
        public static string DefaultAppOfflineTemplate => GetDefaultAppOfflineTemplate();

        private static string GetDefaultAppOfflineTemplate()
        {
            using (var templateStream = typeof(BaseAppOfflineTemplateProvider).Assembly.GetManifestResourceStream("Nuke.WebDeploy.Resources.DefaultAppOffline.html"))
            {
                if (templateStream == null)
                {
                    return "App Temporary Offline for Maintenance";
                }

                using (var streamReader = new StreamReader(templateStream))
                {
                    var template = streamReader.ReadToEnd();
                    return template;
                }
            }
        }
    }
}
