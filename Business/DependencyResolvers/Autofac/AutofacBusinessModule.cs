using Autofac;
using Castle.DynamicProxy;
using Autofac.Extras.DynamicProxy;
using Business.Concrete;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<EFCarDal>().As<ICarDal>().SingleInstance();

            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EFBrandDal>().As<IBrandDal>().SingleInstance();

            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EFColorDal>().As<IColorDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EFUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EFCustomerDal>().As<ICustomerDal>().SingleInstance();

            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<EFRentalDal>().As<IRentalDal>().SingleInstance();

            builder.RegisterType<CarImageManager>().As<ICarImageService>().SingleInstance();
            builder.RegisterType<EFCarImageDal>().As<ICarImageDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
