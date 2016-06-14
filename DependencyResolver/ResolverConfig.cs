using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;
using ORM;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using BLL.Interfaces.Services;
using BLL.Services;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<ApplicationDbContext>().InSingletonScope();
            }

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            kernel.Bind<IRatingRepository>().To<RatingRepository>();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IPhotoService>().To<PhotoService>();
            kernel.Bind<IRatingService>().To<RatingService>();
        }
    }
}
