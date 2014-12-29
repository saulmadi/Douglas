using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using BlingBag;

namespace Douglas.Web.Api.Infrastructure.Configuration
{
    public class AutoFacBlingDispatcher : BlingDispatcherBase
    {
        readonly ILifetimeScope _container;

        public AutoFacBlingDispatcher(ILifetimeScope container)
        {
            _container = container;
        }

        protected override IEnumerable ResolveAll(Type blingHandlerType)
        {
            Type serviceType = typeof(IEnumerable<>).MakeGenericType(blingHandlerType);
            var handlers = _container.Resolve(serviceType) as IEnumerable;
            return handlers;
        }
    }
}