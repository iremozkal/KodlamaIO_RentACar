using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        IDataResult<RentalDetailDto> GetRentalDto(Expression<Func<Rental, bool>> filter = null);
        IDataResult<List<RentalDetailDto>> GetAllRentalDetails(Expression<Func<Rental, bool>> filter = null);
    }
}
