using System;
using System.Diagnostics;
using Common.Logging;
using Parantez.Core.Configuration;
using Parantez.Core.DependencyResolution;
using Parantez.Core.Logging;
using Parantez.Core.BackOrderPipeline.Middleware;
using Parantez.Core.BackOrderPipeline.Request;
using Parantez.Core.BackOrderPipeline.Response;
using Parantez.Core.Plugins;

namespace Parantez.Core
{
    internal class ParantezCore : IParantezCore
    {
        private readonly IConfigReader _configReader;
        private readonly ILog _log;
        private readonly IParantezContainer _container;
        private readonly AverageStat _averageResponse;

        public ParantezCore(IConfigReader configReader, ILog log, IParantezContainer container)
        {
            _configReader = configReader;
            _log = log;
            _container = container;
            _averageResponse = new AverageStat("milliseconds");
        }
        
        private void StartPlugins()
        {
            IPlugin[] plugins = _container.GetPlugins();
            foreach (IPlugin plugin in plugins)
            {
                plugin.Start();
            }
        }
        
        private void StopPlugins()
        {
            IPlugin[] plugins = _container.GetPlugins();
            foreach (IPlugin plugin in plugins)
            {
                plugin.Stop();
            }
        }

        public ResponseMessage Calculate(RequestMessage req)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _log.Info($"[request catch '{req.UserChannel} - Value {req.Value}']");
            IMiddleware pipeline = _container.GetMiddlewarePipeline();
            try
            {
                var res = pipeline.Invoke(req);
                stopwatch.Stop();
                _log.Info($"[Message ended - Took {stopwatch.ElapsedMilliseconds} milliseconds]");
                _averageResponse.Log(stopwatch.ElapsedMilliseconds);
                return res;
            }
            catch (Exception ex)
            {
                _log.Error($"ERROR WHILE PROCESSING MESSAGE: {ex}");
            }

            stopwatch.Stop();

            _log.Info($"[Message ended - Took {stopwatch.ElapsedMilliseconds} milliseconds]");
            _averageResponse.Log(stopwatch.ElapsedMilliseconds);
            return null;
        }
    }
}