using System.IO;

namespace Nuke.WebDeploy
{
    public static class DefaultAppOfflineProvider
    {
        public static string DefaultAppOffline => GetDefaultAppOffline();

        private static string GetDefaultAppOffline()
        {
            using (var templateStream = typeof(DefaultAppOfflineProvider).Assembly.GetManifestResourceStream("Nuke.WebDeploy.Resources.DefaultAppOffline.html"))
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
