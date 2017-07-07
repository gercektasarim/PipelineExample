using System;
using System.Linq;
using System.Threading;
using Common.Logging;
using Parantez.Core.BackOrderPipeline.Response;

namespace Parantez.Core.Plugins.StandardPlugins
{
    internal class ConnectionPlugin : IPlugin
    {
        private const string EXPECTED_ERROR_MESSAGE = "Can Not Starded...'";
        private readonly ParantezCore _ParantezCore;
        private readonly ILog _log;

        public ConnectionPlugin(ParantezCore ParantezCore, ILog log)
        {
            _ParantezCore = ParantezCore;
            _log = log;
        }

        public void Start()
        {
            _log.Info("Starting connection health plugin...");
        }

        public void Stop()
        {
            _log.Info("Stopping connection health plugin...");
        }
    }
}