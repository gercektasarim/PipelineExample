using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Logging;
using Parantez.Core.Configuration;
using Parantez.Core.Logging;
using Parantez.Core.BackOrderPipeline.Middleware;
using Parantez.Core.BackOrderPipeline.Middleware.StandardMiddleware;
using Parantez.Core.BackOrderPipeline.Middleware.Multinet;
using Parantez.Core.Plugins.StandardPlugins;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;

namespace Parantez.Core.DependencyResolution
{
    public class ContainerFactory : IContainerFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigReader _configReader;
        private readonly ILog _logger;

        private readonly Type[] _singletons =
        {
            typeof(IParantezCore),
            typeof(ParantezCore),
            typeof(IConfigReader),
        };

        public ContainerFactory(IConfiguration configuration, IConfigReader configReader)
            : this(configuration, configReader, null)
        { }

        public ContainerFactory(IConfiguration configuration, IConfigReader configReader, ILog logger)
        {
            _configuration = configuration;
            _configReader = configReader;
            _logger = logger;
        }

        public IParantezContainer CreateContainer()
        {
            Registry registry = CreateRegistry();

            SetupSingletons(registry);
            SetupMiddlewarePipeline(registry);
            Type[] pluginTypes = SetupPlugins(registry);

            registry.For<IParantezCore>().Use(x => x.GetInstance<ParantezCore>());
            registry.For<ILog>().Use(() => _logger);
            registry.For<IConfigReader>().Use(() => _configReader);

            IParantezContainer container = CreateContainer(pluginTypes, registry);
            return container;
        }

        private static Registry CreateRegistry()
        {
            var registry = new Registry();

            // setups DI for everything in Parantez.Core
            registry.Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });

            return registry;
        }

        private static IParantezContainer CreateContainer(Type[] pluginTypes, Registry registry)
        {
            var container = new ParantezContainer(pluginTypes);
            registry.For<IParantezContainer>().Use(x => container);
            container.Initialise(registry);
            return container;
        }

        private void SetupSingletons(Registry registry)
        {
            foreach (Type type in _singletons)
            {
                registry.For(type).Singleton();
            }
        }

        private void SetupMiddlewarePipeline(Registry registry)
        {
            Stack<Type> pipeline = GetPipelineStack();

            registry.Scan(x =>
            {
                x.WithDefaultConventions();

                // scan assemblies that we are loading pipelines from
                foreach (Type middlewareType in pipeline)
                {
                    x.AssemblyContainingType(middlewareType);
                }
            });

            registry.For<IMiddleware>().Use<UnhandledMessageMiddleware>();
            

            while (pipeline.Any())
            {
                Type nextType = pipeline.Pop();
                var nextDeclare = registry.For<IMiddleware>();

                // using reflection as Structuremap doesn't allow passing types in at the moment :-(
                MethodInfo decorateMethod = nextDeclare.GetType().GetMethod("DecorateAllWith", new[] { typeof(Func<Instance, bool>) });
                MethodInfo generic = decorateMethod.MakeGenericMethod(nextType);
                generic.Invoke(nextDeclare, new object[] { null });
            }

            registry.For<IMiddleware>().DecorateAllWith<MultinetMiddleware>();
        }

        private Stack<Type> GetPipelineStack()
        {
            Type[] pipelineList = _configuration.ListMiddlewareTypes() ?? new Type[0];

            var pipeline = new Stack<Type>();
            foreach (Type type in pipelineList)
            {
                pipeline.Push(type);
            }

            return pipeline;
        }

        private Type[] SetupPlugins(Registry registry)
        {
            var pluginTypes = new List<Type>
            {
                typeof (StatsPlugin),
                typeof (ConnectionPlugin)
            };

            Type[] customPlugins = _configuration.ListPluginTypes() ?? new Type[0];
            pluginTypes.AddRange(customPlugins);

            registry.Scan(x =>
            {
                // scan assemblies that we are loading pipelines from
                foreach (Type pluginType in pluginTypes)
                {
                    x.AssemblyContainingType(pluginType);
                    x.WithDefaultConventions();
                }
            });

            // make all plugins singletons
            foreach (Type pluginType in pluginTypes)
            {
                registry
                    .For(pluginType)
                    .Singleton();
            }

            return pluginTypes.ToArray();
        }
    }
}