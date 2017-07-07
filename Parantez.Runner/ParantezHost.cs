using System;
using Common.Logging;
using Parantez.Core;
using Parantez.Core.Configuration;
using Parantez.Core.DependencyResolution;
using Parantez.Core.BackOrderPipeline.Request;

namespace Parantez.Runner
{
    /// <summary>
    /// ParantezHost is required due to TopShelf.
    /// </summary>
    public class ParantezHost
    {
        private readonly IConfigReader _configReader;
        private readonly ILog _logger;
        private IParantezCore _ParantezCore;

        public ParantezHost(IConfigReader configReader)
        {
            _configReader = configReader;
            _logger = LogManager.GetLogger(GetType());
        }

        public void Start()
        {
            IContainerFactory containerFactory = new ContainerFactory(new ConfigurationBase(), _configReader, _logger);
            IParantezContainer container = containerFactory.CreateContainer();
            _ParantezCore = container.GetParantezCore();
            while (true)
            {

            bool i = true;
            var val = 0;
            while (i)
            {
                if(int.TryParse(Console.ReadLine(),out val))
                    i = false;
                if (i)
                    Console.WriteLine("integer bir sayı girin.");
            }

            var retVal = _ParantezCore.Calculate(new RequestMessage { UserChannel = RequestCorp.MultinetCorp, UserEmail = "email@email.com", UserId = "1", Username = "1", Value = val });
            Console.WriteLine($"Multinet İçin {retVal.Text}");

            }
        }

        public void Stop()
        {
            Console.WriteLine("Disconnecting...");
            Environment.Exit(0);
        }
    }
}