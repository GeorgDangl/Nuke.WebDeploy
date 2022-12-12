using System.Diagnostics;
using Microsoft.Web.Deployment;
using Nuke.Common;
using Serilog;

namespace Nuke.WebDeploy
{
    public static class WebDeployLogger
    {
        public static void DestinationOptions_Trace(object sender, DeploymentTraceEventArgs e)
        {
            switch (e.EventLevel)
            {
                case TraceLevel.Error:
                    Log.Error(e.Message);
                    break;

                case TraceLevel.Warning:
                    Log.Warning(e.Message);
                    break;

                case TraceLevel.Info:
                    Log.Information(e.Message);
                    break;

                case TraceLevel.Verbose:
                    Log.Verbose(e.Message);
                    break;
            }
        }
    }
}