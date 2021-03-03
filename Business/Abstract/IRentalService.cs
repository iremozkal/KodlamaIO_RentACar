using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IRentalService : IService<Rental>
    {
        IResult IsReturn(int id);
        IDataResult<RentalDetailDto> GetRentalDto(Expression<Func<Rental, bool>> filter = null);
    }
}
