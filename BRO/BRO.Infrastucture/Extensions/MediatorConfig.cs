using Autofac;
using BRO.Domain;
using BRO.Domain.Command;
using BRO.Domain.Command.Category;
using BRO.Domain.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Extensions
{
    public static class MediatorConfig
    {
        public static void ConfigureMediator(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            containerBuilder.Register(factory =>
            {
                var lifetimeScope = factory.Resolve<ILifetimeScope>();
                return new AutofacDependencyResolver(lifetimeScope.BeginLifetimeScope());

            }).As<IDependencyResolver>().InstancePerLifetimeScope();
            var handlersAssembly = typeof(AddCategoryCommandHandler).Assembly;
            containerBuilder.RegisterAssemblyTypes(handlersAssembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            containerBuilder.RegisterAssemblyTypes(handlersAssembly).AsClosedTypesOf(typeof(IQueryHandler<,>)).InstancePerLifetimeScope();
        }
    }
}
