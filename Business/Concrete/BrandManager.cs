using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<Brand> Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessDataResult<Brand>(brand, Messages.AddSuccess);
        }

        public IDataResult<Brand> Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessDataResult<Brand>(brand, Messages.UpdateSuccess);
        }

        public IDataResult<Brand> Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessDataResult<Brand>(brand, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(_brandDal.IsExists(x => x.Id == id));
        }

        public IDataResult<Brand> GetById(int id)
        {
            var result = _brandDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Brand>(result);
            else
                return new ErrorDataResult<Brand>(result, Messages.BrandNotExist);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return _brandDal.GetCount();
        }

        public void WriteAll(List<Brand> brandList)
        {
            foreach (Brand b in brandList)
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}", b.Id, b.Name);
            Console.WriteLine();
        }
    }
}
