using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RssReader.Core.Classes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceObjects.Classes.Services
{
    public class DIService
    {
        static IContainer _container;

        public static IContainer Container { get
            {
                return _container;
            }
        }
        public static IContainer ConfigureContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacConfigModule>();
            builder.Populate(services);
            builder.RegisterType(typeof(ReaderDbContext)).AsSelf().InstancePerLifetimeScope();
            _container = builder.Build();
            return _container;

        }
    }
}
