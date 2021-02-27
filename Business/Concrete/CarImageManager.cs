using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Arabanın resmi yoksa
//File.Copy(carImage.ImagePath, Paths.DefaultImagePath);
//carImage.ImagePath = Paths.DefaultImagePath;
//carImage.Date = DateTime.Now;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<CarImage> Add_WebAPI(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarImageCountExceeded(carImage.CarId),
                CheckIfFileExtensionCorrect(carImage.ImagePath)
            );

            if (result != null) return new ErrorDataResult<CarImage>(carImage, result.Message);

            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);

            return new SuccessDataResult<CarImage>(carImage, Messages.AddSuccess);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IDataResult<CarImage> Update_WebAPI(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarImageCountExceeded(carImage.CarId),
                CheckIfFileExtensionCorrect(carImage.ImagePath),
                IsExistById(carImage.Id)
            );

            if (result != null) return new ErrorDataResult<CarImage>(carImage, result.Message);

            carImage.ImagePath = FileHelper.Update(_carImageDal.Get(p => p.Id == carImage.Id).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);

            return new SuccessDataResult<CarImage>(carImage, Messages.UpdateSuccess);
        }

        public IDataResult<CarImage> Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            File.Delete(carImage.ImagePath);

            return new SuccessDataResult<CarImage>(carImage, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(_carImageDal.IsExists(x => x.Id == id));
        }

        public IDataResult<CarImage> GetById(int id)
        {
            var result = _carImageDal.Get(c => c.Id == id);

            if (result != null)
                return new SuccessDataResult<CarImage>(result);
            else
                return new ErrorDataResult<CarImage>(result, Messages.ImageNotFound);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return _carImageDal.GetCount();
        }

        public IDataResult<List<CarImage>> GetAllCarImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            if (result.Count == 0)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage> {
                    new CarImage { CarId = carId, ImagePath = Paths.DefaultImagePath, Date = DateTime.Now } }, Messages.DefaultImageAdded);
            }

            return new SuccessDataResult<List<CarImage>>(result, Messages.ChosenImageAdded);
        }

        public void WriteAll(List<CarImage> carImageList)
        {
            foreach (CarImage c in carImageList)
                Console.WriteLine("ID: #{0,-5}   CarID: {1,-5}   Date: {2,-10}   Path: {3}",
                    c.Id, c.CarId, c.Date.ToShortDateString(), c.ImagePath);
            Console.WriteLine();
        }

        #region Business Rules
        private IResult CheckIfCarImageCountExceeded(int carId)
        {
            var result = _carImageDal.GetAll(x => x.CarId == carId);
            if (result.Count > 5)
            {
                return new ErrorResult(Messages.CarImageCountError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfFileExtensionCorrect(string path)
        {
            List<string> extensions = new List<string> { ".png", ".jpeg", ".jpg" };

            if (!extensions.Contains(Path.GetExtension(path).ToLower()))
            {
                return new ErrorResult(Messages.IncorrectFileExtension);
            }
            return new SuccessResult();
        }
        #endregion

        public IDataResult<CarImage> Add(CarImage carImage)
        {
            throw new NotImplementedException();

            //IResult result = BusinessRules.Run(
            //  CheckIfCarImageCountExceeded(carImage.CarId)
            //);

            //if (result != null)
            //    return new ErrorDataResult<CarImage>(carImage, result.Message);

            //_carImageDal.Add(carImage);
            //return new SuccessDataResult<CarImage>(carImage, Messages.AddSuccess);
        }

        public IDataResult<CarImage> Update(CarImage carImage)
        {
            throw new NotImplementedException();

            //IResult result = BusinessRules.Run(
            //  CheckIfCarImageCountExceeded(carImage.CarId)
            //);

            //if (result != null)
            //    return new ErrorDataResult<CarImage>(carImage, result.Message);

            //_carImageDal.Update(carImage);
            //return new SuccessDataResult<CarImage>(carImage, Messages.UpdateSuccess);
        }
    }
}
