using Parantez.Core.BackOrderPipeline.Response;
using Parantez.Core.BackOrderPipeline.Request;

namespace Parantez.Core
{
    public interface IParantezCore
    {
        ResponseMessage Calculate(RequestMessage req); 
    }
}