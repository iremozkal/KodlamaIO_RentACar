﻿using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                             on customer.UserId equals user.UserId
                             select new CustomerDetailDto
                             {
                                 Id = user.UserId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 CompanyName = customer.CompanyName
                             };

                return new SuccessDataResult<CustomerDetailDto>(result.FirstOrDefault());
            }
        }

        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var result = from customer in filter == null ? context.Customers : context.Customers.Where(filter)
                             join user in context.Users
                             on customer.UserId equals user.UserId
                             select new CustomerDetailDto
                             {
                                 Id = user.UserId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 CompanyName = customer.CompanyName
                             };

                return result.ToList();
            }
        }
    }
}