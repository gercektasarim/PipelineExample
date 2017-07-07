using Parantez.Core.BackOrderPipeline.Response;

namespace Parantez.Core.BackOrderPipeline.Request
{
    public class RequestMessage
    {
        public int Value { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public RequestCorp UserChannel { get; set; }
    }
}