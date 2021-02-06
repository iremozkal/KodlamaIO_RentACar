using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarManager : IManager<Car>
    {
        Car GetCarById(int id);
        int GetCountOfAllCars();
        List<Car> GetAllCars();
        List<Car> GetAllCarsByColorId(int id);
        List<Car> GetAllCarsByBrandId(int id);
        List<Car> GetAllCarsByDailyPrice(decimal min, decimal max);
        List<Car> GetAllCarsByModelYear(int year);
    }
}
