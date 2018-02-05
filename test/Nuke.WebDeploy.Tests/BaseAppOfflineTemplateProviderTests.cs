using Xunit;

namespace Nuke.WebDeploy.Tests
{
    public class BaseAppOfflineTemplateProviderTests
    {
        [Fact]
        public void ReturnsDefaultTemplate()
        {
            var defaultAppOffline = BaseAppOfflineTemplateProvider.DefaultAppOfflineTemplate;
            Assert.False(string.IsNullOrWhiteSpace(defaultAppOffline));
            Assert.Contains("<html", defaultAppOffline);
            Assert.Contains("App Offline - Maintenance", defaultAppOffline);
        }
    }
}
