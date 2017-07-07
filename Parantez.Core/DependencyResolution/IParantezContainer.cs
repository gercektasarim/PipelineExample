using Parantez.Core.BackOrderPipeline.Middleware;
using Parantez.Core.Plugins;
using StructureMap;

namespace Parantez.Core.DependencyResolution
{
    public interface IParantezContainer
    {
        IParantezCore GetParantezCore();
        IPlugin[] GetPlugins();
        T GetPlugin<T>() where T : class, IPlugin;
        IMiddleware GetMiddlewarePipeline();
        IContainer GetStructuremapContainer();
    }
}