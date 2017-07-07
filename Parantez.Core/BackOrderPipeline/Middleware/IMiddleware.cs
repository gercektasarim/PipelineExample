using System.Collections.Generic;
using Parantez.Core.BackOrderPipeline.Request;
using Parantez.Core.BackOrderPipeline.Response;

namespace Parantez.Core.BackOrderPipeline.Middleware
{
    public interface IMiddleware
    {
        ResponseMessage Invoke(RequestMessage message);
        IEnumerable<CommandDescription> GetSupportedCommands();
    }
}