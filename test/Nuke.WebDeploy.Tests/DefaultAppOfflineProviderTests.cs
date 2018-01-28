using Xunit;

namespace Nuke.WebDeploy.Tests
{
    public class DefaultAppOfflineProviderTests
    {
        [Fact]
        public void ReturnsDefaultTemplate()
        {
            var defaultAppOffline = DefaultAppOfflineProvider.DefaultAppOffline;
            Assert.False(string.IsNullOrWhiteSpace(defaultAppOffline));
            Assert.Contains("<html", defaultAppOffline);
            Assert.Contains("App Offline - Maintenancel", defaultAppOffline);
        }
    }
}
