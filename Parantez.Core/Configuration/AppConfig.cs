using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Parantez.Core.Configuration
{
    public class AppConfig
    {
        // CommonConfig
        public static Commonconfig CommonConfig = new Commonconfig();
    }

    public class Commonconfig
    {
        public void Initialize()
        {
            this.CommonInitialize();
        }

        public void Initialize(string[] args)
        {
            this.CommonInitialize(args);
        }

        private void CommonInitialize(string[] args = null)
        {
           // ElasticSearchServer = ConfigurationManager.AppSettings.Get("ElasticSearchServer");
           
        }

        public string ElasticSearchServer { get; set; }
        
    }
}
