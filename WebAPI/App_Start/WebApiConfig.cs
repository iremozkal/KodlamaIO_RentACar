using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using WebAPI.Models;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

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

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
