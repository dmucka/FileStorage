using Autofac;
using AutoMapper;
using FileStorageBL.Facades;
using FileStorageBL.QueryObjects;
using FileStorageBL.Services;
using FileStorageDAL.Config;
using System.Linq;
using System.Reflection;

namespace FileStorageBL.Config
{
    public class BusinessLayerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {

            containerBuilder.RegisterModule(new DataAccessLayerModule());

            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsSubclassOf(typeof(BaseQueryObject)))
                .AsSelf()
                .InstancePerDependency();

            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsSubclassOf(typeof(BaseService)))
                .AsSelf()
                .InstancePerDependency();

            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsSubclassOf(typeof(BaseFacade)))
                .AsSelf()
                .InstancePerDependency();

            containerBuilder.RegisterInstance(new Mapper(new MapperConfiguration(BusinessMappingConfig.ConfigureMapping)))
                .As<IMapper>()
                .SingleInstance();
        }
    }
}