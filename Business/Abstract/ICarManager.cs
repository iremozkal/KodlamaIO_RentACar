using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarManager
    {
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
        Car GetCarById(int id);
        int GetCountOfAllCars();
        bool IsExistById(int id);
        List<Car> GetAllCars();
        List<Car> GetAllCarsByColorId(int id);
        List<Car> GetAllCarsByBrandId(int id);
        List<Car> GetAllCarsByDailyPrice(decimal min, decimal max);
        List<Car> GetAllCarsByModelYear(int year);
        void WriteAll(List<Car> carList);
    }
}
