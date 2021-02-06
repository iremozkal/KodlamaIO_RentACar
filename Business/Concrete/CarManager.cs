using Business.Abstract;
using Business.Validators;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarManager
    {
        private readonly ICarDal carDal;
        private readonly CarValidator carValidator;

        public CarManager(ICarDal _carDal)
        {
            carValidator = new CarValidator();
            carDal = _carDal;
        }

        public void Add(Car car)
        {
            ValidationResult result = this.carValidator.Validate(car);

            if (result.IsValid)
            {
                this.carDal.Add(car);
                Console.WriteLine("(+) Insert operation is succesfully done.");
            }
            else
            {
                foreach (var error in result.Errors)
                    Console.WriteLine("(-) Operation failed! " + error.ErrorMessage);
            }
        }

        public void Update(Car car)
        {
            ValidationResult result = this.carValidator.Validate(car);

            if (result.IsValid)
            {
                this.carDal.Update(car);
                Console.WriteLine("(+) Update operation is succesfully done.");
            }
            else
            {
                foreach (var error in result.Errors)
                    Console.WriteLine("(-) Operation failed! " + error.ErrorMessage);
            }
        }

        public void Delete(Car car)
        {
            this.carDal.Delete(car);
            Console.WriteLine("(+) Delete operation is succesfully done.");
        }

        public Car GetCarById(int id)
        {
            return this.carDal.Get(c => c.Id == id);
        }

        public int GetCountOfAllCars()
        {
            return this.carDal.GetCount();
        }

        public bool IsExistById(int id)
        {
            return this.carDal.IsExists(x=>x.Id == id);
        }

        public List<Car> GetAllCars()
        {
            return this.carDal.GetAll();
        }

        public List<Car> GetAllCarsByBrandId(int id)
        {
            return this.carDal.GetAll(c => c.BrandId == id);
        }

        public List<Car> GetAllCarsByColorId(int id)
        {
            return this.carDal.GetAll(c => c.ColorId == id);

        }

        public List<Car> GetAllCarsByDailyPrice(decimal min, decimal max)
        {
            return this.carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max);

        }

        public List<Car> GetAllCarsByModelYear(int year)
        {
            return this.carDal.GetAll(c => c.ModelYear == year);
        }

        public void WriteAll(List<Car> carList)
        {
            foreach (Car c in carList)
                Console.WriteLine("ID: #{0}   BrandID: {1}   ColorID: {2}   Model: {3}   Fiyat: {4}   Açıklama: {5}",
                    c.Id, c.BrandId, c.ColorId, c.ModelYear, c.DailyPrice.ToString("0.00"), c.Description);
            Console.WriteLine();
        }
    }
}
