using System;
using System.Collections.Generic;
using System.Linq;
using Parantez.Core.BackOrderPipeline.Request;
using Parantez.Core.BackOrderPipeline.Response;

namespace Parantez.Core.BackOrderPipeline.Middleware
{
    public abstract  class MiddlewareBase : IMiddleware
    {
        protected HandlerMapping[] HandlerMappings;
        private readonly IMiddleware _next;

        protected MiddlewareBase(IMiddleware next)
        {
            _next = next;
            HandlerMappings = HandlerMappings ?? new HandlerMapping[0];
        }

        protected ResponseMessage Next(RequestMessage message)
        {
            return _next.Invoke(message);
        }

        public virtual ResponseMessage Invoke(RequestMessage message)
        {
            foreach (var handlerMapping in HandlerMappings.Where(x => x.ValidHandles[0] == (int)message.UserChannel))
            {
                if(handlerMapping.ValidHandles[1] <= message.Value && handlerMapping.ValidHandles[2] > message.Value)
                {
                    return handlerMapping.EvaluatorFunc(message, handlerMapping.ValidHandles[0]);
                }
            }
            return new ResponseMessage { Text = "Wrong Request" };
        }

        public IEnumerable<CommandDescription> GetSupportedCommands()
        {
            foreach (var handlerMapping in HandlerMappings)
            {
                yield return new CommandDescription
                {
                    Command = string.Join(" | ", handlerMapping.ValidHandles.Select(x => $"`{x}`").OrderBy(x => x)),
                    Description = handlerMapping.Description
                };
            }

            foreach (var commandDescription in _next.GetSupportedCommands())
            {
                yield return commandDescription;
            }
        }
    }
}