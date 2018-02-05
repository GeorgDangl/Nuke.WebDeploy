using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Core.Tooling;

namespace Nuke.WebDeploy
{
    internal class WebDeployProcess : IProcess
    {
        public WebDeployProcess(bool wasSuccessful)
        {
            ExitCode = wasSuccessful ? 0 : 1;
        }

        public void Dispose()
        {
        }

        public string FileName => throw new NotSupportedException();

        public string Arguments => throw new NotSupportedException();

        public string WorkingDirectory => throw new NotSupportedException();

        public IEnumerable<Output> Output => Enumerable.Empty<Output>();

        public int ExitCode { get; }

        public void Kill()
        {
        }

        public bool WaitForExit()
        {
            return true;
        }
    }
}
