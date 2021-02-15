using Business.Abstract;
using Business.Constants;
using Business.Validators;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal carDal;
        private readonly CarValidator carValidator;

        public CarManager(ICarDal _carDal)
        {
            carValidator = new CarValidator();
            carDal = _carDal;
        }

        public IDataResult<Car> Add(Car car)
        {
            ValidationResult result = this.carValidator.Validate(car);

            if (result.IsValid)
            {
                this.carDal.Add(car);
                return new SuccessDataResult<Car>(car, Messages.AddSuccess);
            }
            else
            {
                var errorMessage = Messages.AddError + "\n";
                foreach (var error in result.Errors) errorMessage += error.ErrorMessage + "\n";
                return new ErrorDataResult<Car>(car, errorMessage);
            }
        }

        public IDataResult<Car> Update(Car car)
        {
            ValidationResult result = this.carValidator.Validate(car);

            if (result.IsValid)
            {
                this.carDal.Update(car);
                return new SuccessDataResult<Car>(car, Messages.UpdateSuccess);
            }
            else
            {
                var errorMessage = Messages.UpdateError + "\n";
                foreach (var error in result.Errors) errorMessage += error.ErrorMessage + "\n";
                return new ErrorDataResult<Car>(car, errorMessage);
            }
        }

        public IDataResult<Car> Delete(Car car)
        {
            this.carDal.Delete(car);
            return new SuccessDataResult<Car>(car, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.carDal.IsExists(x => x.Id == id));
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = this.carDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Car>(result);
            else
                return new ErrorDataResult<Car>(result, "NotFound");
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(this.carDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return this.carDal.GetCount();
        }

        public IDataResult<List<Car>> GetAllCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(this.carDal.GetAll(c => c.BrandId == id));
        }

        public IDataResult<List<Car>> GetAllCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(this.carDal.GetAll(c => c.ColorId == id));

        }

        public IDataResult<List<Car>> GetAllCarsByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(this.carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max));

        }

        public IDataResult<List<Car>> GetAllCarsByModelYear(int year)
        {
            return new SuccessDataResult<List<Car>>(this.carDal.GetAll(c => c.ModelYear == year));
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            return new SuccessDataResult<List<CarDetailDto>>(this.carDal.GetCarDetails(filter));
        }

        public void WriteAll(List<Car> carList)
        {
            foreach (Car c in carList)
                Console.WriteLine("ID: #{0,-5}   BrandID: {1,-10}   ColorID: {2,-10}   Model: {3,-10}   Price: {4,-10}   Description: {5}",
                    c.Id, c.BrandId, c.ColorId, c.ModelYear, c.DailyPrice.ToString("0.00"), c.Description);
            Console.WriteLine();
        }

        public void WriteAllCarDetails(List<CarDetailDto> carDtoList)
        {
            foreach (CarDetailDto c in carDtoList)
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}   Brand: {2,-10}   Color: {3,-10}   Daily Price: {4}",
                    c.CarId, c.CarName, c.BrandName, c.ColorName, c.DailyPrice.ToString("0.00"));
            Console.WriteLine();
        }
    }
}
