using System.Diagnostics;
using Microsoft.Web.Deployment;
using Nuke.Core;

namespace Nuke.WebDeploy
{
    public static class WebDeployLogger
    {
        public static void DestinationOptions_Trace(object sender, DeploymentTraceEventArgs e)
        {
            switch (e.EventLevel)
            {
                case TraceLevel.Error:
                    Logger.Error(e.Message);
                    break;

                case TraceLevel.Warning:
                    Logger.Warn(e.Message);
                    break;

                case TraceLevel.Info:
                    Logger.Info(e.Message);
                    break;

                case TraceLevel.Verbose:
                    Logger.Trace(e.Message);
                    break;
            }
        }
    }
}
