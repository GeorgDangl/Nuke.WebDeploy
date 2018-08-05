using Xunit;

namespace Nuke.WebDeploy.Tests.Regression
{
    public class WebDeployTasksGeneration
    {
        [Fact]
        public void HasGeneratedExtensionMethodsToConfigureProperties()
        {
            var setting = new WebDeploySettings().SetPublishUrl("https://example.com");
            Assert.NotNull(setting);
            Assert.Equal("https://example.com", setting.PublishUrl);
        }
    }
}
