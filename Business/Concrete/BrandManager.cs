using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal brandDal;

        public BrandManager(IBrandDal _brandDal)
        {
            brandDal = _brandDal;
        }

        public IDataResult<Brand> Add(Brand brand)
        {
            this.brandDal.Add(brand);
            return new SuccessDataResult<Brand>(brand, Messages.AddSuccess);
        }

        public IDataResult<Brand> Update(Brand brand)
        {
            this.brandDal.Update(brand);
            return new SuccessDataResult<Brand>(brand, Messages.UpdateSuccess);
        }

        public IDataResult<Brand> Delete(Brand brand)
        {
            this.brandDal.Delete(brand);
            return new SuccessDataResult<Brand>(brand, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.brandDal.IsExists(x => x.Id == id));
        }

        public IDataResult<Brand> GetById(int id)
        {
            var result = this.brandDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Brand>(result);
            else
                return new ErrorDataResult<Brand>(result, "NotFound");
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(this.brandDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return this.brandDal.GetCount();
        }

        public void WriteAll(List<Brand> brandList)
        {
            foreach (Brand b in brandList)
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}", b.Id, b.Name);
            Console.WriteLine();
        }
    }
}
