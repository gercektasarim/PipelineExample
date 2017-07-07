using System.Collections.Generic;
using Common.Logging;
using Parantez.Core.BackOrderPipeline.Request;
using Parantez.Core.BackOrderPipeline.Response;

namespace Parantez.Core.BackOrderPipeline.Middleware.StandardMiddleware
{
    internal class UnhandledMessageMiddleware : IMiddleware
    {
        private readonly ILog _log;

        public UnhandledMessageMiddleware(ILog log)
        {
            _log = log;
        }

        public ResponseMessage Invoke(RequestMessage message)
        {
            _log.Info("Unhandled");
            return new ResponseMessage();
        }

        public IEnumerable<CommandDescription> GetSupportedCommands()
        {
            return new CommandDescription[0];
        }
    }
}