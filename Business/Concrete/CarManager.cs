using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IBrandService _brandService;

        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("car.add")]
        public IDataResult<Car> Add(Car car)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfCarDescriptionIsExists(car.Description),
                CheckIfBrandLimitExceded()
            );

            if (result != null) return new ErrorDataResult<Car>(car, result.Message);

            _carDal.Add(car);
            return new SuccessDataResult<Car>(car, Messages.AddSuccess);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IDataResult<Car> Update(Car car)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarCountOfBrandCorrect(car.BrandId),
                CheckIfCarDescriptionIsExists(car.Description),
                CheckIfBrandLimitExceded()
            );

            if (result != null) return new ErrorDataResult<Car>(car, result.Message);

            _carDal.Update(car);
            return new SuccessDataResult<Car>(car, Messages.UpdateSuccess);
        }

        public IDataResult<Car> Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessDataResult<Car>(car, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(_carDal.IsExists(x => x.Id == id));
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Car>(result);
            else
                return new ErrorDataResult<Car>(result, "NotFound");
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return _carDal.GetCount();
        }

        public IDataResult<List<Car>> GetAllCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }

        public IDataResult<List<Car>> GetAllCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));

        }

        public IDataResult<List<Car>> GetAllCarsByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max));

        }

        public IDataResult<List<Car>> GetAllCarsByModelYear(int year)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ModelYear == year));
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(filter));
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
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}   Brand: {2,-10}   Color: {3,-10}   Model Year: {4,-10}   Daily Price: {5}",
                    c.CarId, c.CarName, c.BrandName, c.ColorName, c.ModelYear, c.DailyPrice.ToString("0.00"));
            Console.WriteLine();
        }

        #region Business Rules
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(x => x.BrandId == brandId);
            if (result.Count > 5)
                return new ErrorResult(Messages.CarCountOfBrandError);
            return new SuccessResult();
        }

        private IResult CheckIfCarDescriptionIsExists(string description)
        {
            var result = _carDal.IsExists(x => x.Description == description);
            if (result)
                return new ErrorResult(Messages.CarDescriptionAlreadyExists);
            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();

            if (result.Data.Count > 15)
                return new ErrorResult(Messages.BrandLimitExceed);
            return new SuccessResult();
        }
        #endregion
    }
}
