using Parantez.Core.Configuration;
using Parantez.Runner.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Topshelf;

namespace Parantez.Runner
{
    public class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        private static readonly IConfigReader ConfigReader = new ConfigReader();

        public static void Main(string[] args)
        {
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1) return;
            AppConfig.CommonConfig.Initialize();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine($"Parantez assembly version: {Assembly.GetExecutingAssembly().GetName().Version}");

            HostFactory.Run(x =>
            {
                x.Service<ParantezHost>(s =>
                {
                    s.ConstructUsing(name => new ParantezHost(ConfigReader));

                    s.WhenStarted(n =>
                    {
                        n.Start();
                    });

                    s.WhenStopped(n => n.Stop());
                });

                x.RunAsNetworkService();
                x.SetDisplayName("Parantez");
                x.SetServiceName("Parantez");
            });
        }
    }
}
