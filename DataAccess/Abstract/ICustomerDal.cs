using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        IDataResult<CustomerDetailDto> GetCustomerDto(Expression<Func<Customer, bool>> filter = null);
        IDataResult<List<CustomerDetailDto>> GetAllCustomerDetails(Expression<Func<Customer, bool>> filter = null);
    }
}
