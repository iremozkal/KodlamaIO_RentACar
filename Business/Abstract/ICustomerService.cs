using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICustomerService : IService<Customer>
    {
        IDataResult<CustomerDetailDto> GetCustomerDto(Expression<Func<Customer, bool>> filter = null);
    }
}
