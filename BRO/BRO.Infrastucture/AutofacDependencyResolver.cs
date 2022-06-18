

using Autofac;
using BRO.Domain;
using BRO.Domain.Query;
using BRO.Domain.Query.Category;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _lifetimeScope;
    
        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        public T ResolveOrDefault<T>() where T : class
        {
            return _lifetimeScope.ResolveOptional<T>();
        }
    }
}
