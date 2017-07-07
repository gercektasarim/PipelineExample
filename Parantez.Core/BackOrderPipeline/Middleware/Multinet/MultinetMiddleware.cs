using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parantez.Core.BackOrderPipeline.Request;
using Parantez.Core.BackOrderPipeline.Response;
using System;

namespace Parantez.Core.BackOrderPipeline.Middleware.Multinet
{
    internal class MultinetMiddleware : MiddlewareBase
    {
        private readonly IParantezCore _ParantezCore;

        public MultinetMiddleware(IMiddleware next, IParantezCore ParantezCore) : base(next)
        {
            _ParantezCore = ParantezCore;

            Array.Resize(ref HandlerMappings, HandlerMappings.Length + 1);
            HandlerMappings[HandlerMappings.Length - 1] = new HandlerMapping
            {
                //aralıklar birbirinin üstüne gelirse 2 sindende kazanım hesaplaması isteniyorsa bir tık iyileştirilmeli.
                ValidHandles = new[] { (int)RequestCorp.MultinetCorp, 50, 100 },
                Description = "kullanıcının 50 birim üzerindeki harcamalarına 0.25 oranında geri ödeme sağlanması.",
                EvaluatorFunc = Case1
            };

            Array.Resize(ref HandlerMappings, HandlerMappings.Length + 1);
            HandlerMappings[HandlerMappings.Length - 1] = new HandlerMapping
            {
                ValidHandles = new[] { (int)RequestCorp.MultinetCorp, 100, 99999999 },
                Description = "kullanıcının her 100 birimlik harcamasından sonra (tek veya çoklu defada) 5 birimlik geri ödeme sağlanması.",
                EvaluatorFunc = Case2
            };
        }

        private ResponseMessage Case1(RequestMessage message, int matchedHandle)
        {
            Services.CommonService.MultinetCase1(message);
            return new ResponseMessage
            {
                Text = $"{message.Value * 0.025} birim kazanım hesaplandı."
            };
        }

        private ResponseMessage Case2(RequestMessage message, int matchedHandle)
        {
            Services.CommonService.MultinetCase2(message);
            return new ResponseMessage
            {
                Text = $"{Math.Floor((decimal)message.Value / 100) * 5} birim kazanım hesaplandı."
            };
        }
    }
}
