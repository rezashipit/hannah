﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Lifestyle;

namespace Chaos.Web.Dependency
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;
        public WindsorDependencyResolver(IWindsorContainer container)
        {
            _container = container;
        }
        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ?
                _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_container.Kernel.HasComponent(serviceType))
            {
                return new object[0];
            }
            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
        
    }
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            this._container = container;
            this._scope = container.BeginScope();
        }
        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }
            else 
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            this._scope.Dispose();
        }
    }
    public class ApiControllersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container,
        Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
             .BasedOn<ApiController>()
             .LifestylePerWebRequest());
        }
    }
}