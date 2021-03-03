using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCustomerDal : EfEntityRepositoryBase<Customer, CarRentContext>, ICustomerDal
    {
        public IDataResult<CustomerDetailDto> GetCustomerDto(Expression<Func<Customer, bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var result = from customer in filter == null ? context.Customers : context.Customers.Where(filter)
                             join user in context.Users
                             on customer.UserId equals user.Id
                             select new CustomerDetailDto
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 CompanyName = customer.CompanyName
                             };

                return new SuccessDataResult<CustomerDetailDto>(result.FirstOrDefault());
            }
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var result = from customer in filter == null ? context.Customers : context.Customers.Where(filter)
                             join user in context.Users
                             on customer.UserId equals user.Id
                             select new CustomerDetailDto
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 CompanyName = customer.CompanyName
                             };

                return new SuccessDataResult<List<CustomerDetailDto>>(result.ToList());
            }
        }
    }
}
