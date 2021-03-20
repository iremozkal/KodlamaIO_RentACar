using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICarService : IService<Car>
    {
        IDataResult<List<CarDetailDto>> GetAllCarsByColorId(int id);
        IDataResult<List<CarDetailDto>> GetAllCarsByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetAllCarsByDailyPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetAllCarsByModelYear(int year);
        IDataResult<List<CarDetailDto>> GetAllCarDetails(Expression<Func<Car, bool>> filter = null);
        void WriteAllCarDetails(List<CarDetailDto> carDtoList);
    }
}
