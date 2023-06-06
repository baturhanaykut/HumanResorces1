using Autofac;
using AutoMapper;
using HumanResources_Application.AutoMapper;
using HumanResources_Application.Service.AddresServices;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Application.Service.DepartmentService;
using HumanResources_Domain.Repositories;
using HumanResources_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            //builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();

            //builder.RegisterType<ExpenseRepository>().As<IExpenseRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<ExpenseService>().As<IExpenseService>().InstancePerLifetimeScope();

            //builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            //builder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();

            //builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<DistrictRepository>().As<IDistrictRepository>().InstancePerLifetimeScope();

            //builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(AppUserRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(AppUserService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);


        }
    }
}
