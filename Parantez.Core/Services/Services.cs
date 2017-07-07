namespace Parantez.Core.Services
{
    public class Services
    {
        public CommonService CommonService { get; set; }

        public Services()
        {
            this.CommonService = new CommonService();
        }
    }
}