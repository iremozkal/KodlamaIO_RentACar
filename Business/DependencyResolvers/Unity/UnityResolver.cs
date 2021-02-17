using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace Business.DependencyResolvers.Unity
{
    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            Initialize(ref container);
            _container = container;
        }

        public void Initialize(ref IUnityContainer container)
        {
            container.RegisterType<ICarService, CarManager>();
            container.RegisterType<IBrandService, BrandManager>();
            container.RegisterType<IColorService, ColorManager>();
            container.RegisterType<ICustomerService, CustomerManager>();
            container.RegisterType<IUserService, UserManager>();
            container.RegisterType<IRentalService, RentalManager>();
            
            container.RegisterType<ICarDal, EFCarDal>();
            container.RegisterType<IBrandDal, EFBrandDal>();
            container.RegisterType<IColorDal, EFColorDal>();
            container.RegisterType<ICustomerDal, EFCustomerDal>();
            container.RegisterType<IUserDal, EFUserDal>();
            container.RegisterType<IRentalDal, EFRentalDal>();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}