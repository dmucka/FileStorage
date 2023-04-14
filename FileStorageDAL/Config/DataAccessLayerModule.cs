using Autofac;
using FileStorageDAL.Query;
using FileStorageDAL.Repository;
using System;

namespace FileStorageDAL.Config
{
    public class DataAccessLayerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance<Func<FileStorageContext>>(() => new FileStorageContext())
                .AsSelf()
                .SingleInstance();

            containerBuilder.RegisterInstance(new RepositoryFactory())
                .As<IRepositoryFactory>()
                .SingleInstance();

            containerBuilder.RegisterInstance(new QueryFactory())
                .As<IQueryFactory>()
                .SingleInstance();

            containerBuilder.RegisterType<UnitOfWork>()
                .AsSelf()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<FileStorageContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            //containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .Where(t => t.BaseType.Name.Contains("Query"))
            //    .AsSelf()
            //    .InstancePerDependency();
            //
            //containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .Where(t => t.BaseType.Name.Contains("Repository"))
            //    .AsSelf()
            //    .InstancePerDependency();
        }
    }
}