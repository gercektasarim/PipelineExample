using System;
using System.Collections.Generic;
using Parantez.Core.BackOrderPipeline.Request;
using Parantez.Core.BackOrderPipeline.Response;

namespace Parantez.Core.BackOrderPipeline.Middleware
{
    public class HandlerMapping
    {
        public HandlerMapping()
        {
            ValidHandles = new int[0];
        }
        public int[] ValidHandles { get; set; }
        
        public string Description { get; set; }
        
        public Func<RequestMessage, int, ResponseMessage> EvaluatorFunc { get; set; }
    }
}