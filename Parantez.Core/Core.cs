
namespace Parantez.Core
{
    public class Core
    {
        private static Core instance;

        private Core()
        {
            this.Services = new Services.Services();
        }
        public static Core Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Core();
                }
                return instance;
            }
        }

        public Services.Services Services { get; set; }
    }
}
