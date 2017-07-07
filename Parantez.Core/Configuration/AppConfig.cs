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
            EvideaElasticSearchServer = ConfigurationManager.AppSettings.Get("EvideaElasticSearchServer");
            EvideaElasticSearchServerLive = ConfigurationManager.AppSettings.Get("EvideaElasticSearchServerLive");

            HourlyRevenueThresholdPercentage = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HourlyRevenueThresholdPercentage") ?? "0");

            NetScalerIP = ConfigurationManager.AppSettings.Get("NetScalerIP");
            NetScalerUsername = ConfigurationManager.AppSettings.Get("NetScalerUsername");
            NetScalerPassword = ConfigurationManager.AppSettings.Get("NetScalerPassword");

            CouchbaseDefaultBucketName = ConfigurationManager.AppSettings.Get("CouchbaseDefaultBucketName");
            CouchbaseGuvenliBucketName = ConfigurationManager.AppSettings.Get("CouchbaseGuvenliBucketName");
            CouchbaseUsername = ConfigurationManager.AppSettings.Get("CouchbaseUsername");
            CouchbasePassword = ConfigurationManager.AppSettings.Get("CouchbasePassword");

            CouchbaseServerList = new List<Uri>();
            var serverStr = ConfigurationManager.AppSettings.Get("CouchbaseServerList").Split(',').ToList();
            serverStr.ForEach(c => CouchbaseServerList.Add(new Uri(c)));
        }

        public string EvideaElasticSearchServer { get; set; }
        public string EvideaElasticSearchServerLive { get; set; }
        public int HourlyRevenueThresholdPercentage { get; set; }
        public string NetScalerIP { get; set; }
        public string NetScalerUsername { get; set; }
        public string NetScalerPassword { get; set; }
        public List<Uri> CouchbaseServerList { get; set; }
        public string CouchbaseDefaultBucketName { get; set; }
        public string CouchbaseGuvenliBucketName { get; set; }
        public string CouchbaseUsername { get; set; }
        public string CouchbasePassword { get; set; }
    }
}
