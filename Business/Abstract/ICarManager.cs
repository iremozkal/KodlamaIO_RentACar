using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarManager : IManager<Car>
    {
        IDataResult<Car> GetCarById(int id);
        int GetCountOfAllCars();
        IDataResult<List<Car>> GetAllCars();
        IDataResult<List<Car>> GetAllCarsByColorId(int id);
        IDataResult<List<Car>> GetAllCarsByBrandId(int id);
        IDataResult<List<Car>> GetAllCarsByDailyPrice(decimal min, decimal max);
        IDataResult<List<Car>> GetAllCarsByModelYear(int year);
        IDataResult<List<CarDetailDto>> GetAllCarDetails(Expression<Func<Car, bool>> filter = null);
        void WriteAllCarDetails(List<CarDetailDto> carDtoList);
    }
}
