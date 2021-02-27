using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService : IService<CarImage>
    {
        IDataResult<List<CarImage>> GetAllCarImagesByCarId(int carId);
        IDataResult<CarImage> Add_WebAPI(IFormFile file, CarImage carImage);
        IDataResult<CarImage> Update_WebAPI(IFormFile file, CarImage carImage);
    }
}
