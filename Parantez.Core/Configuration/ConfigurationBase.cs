﻿using System;
using System.Collections.Generic;
using Parantez.Core.BackOrderPipeline.Middleware;
using Parantez.Core.Plugins;

namespace Parantez.Core.Configuration
{
    public class ConfigurationBase : IConfiguration
    {
        private readonly List<Type> _pipeline = new List<Type>();
        private readonly List<Type> _plugins = new List<Type>();

        public Type[] ListMiddlewareTypes()
        {
            return _pipeline.ToArray();
        }

        public Type[] ListPluginTypes()
        {
            return _plugins.ToArray();
        }

        protected void UseMiddleware<T>() where T : IMiddleware
        {
            _pipeline.Add(typeof(T));
        }

        protected void UsePlugin<T>() where T : IPlugin
        {
            _plugins.Add(typeof(T));
        }
    }
}