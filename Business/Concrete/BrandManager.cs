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
    public class BrandManager : IBrandManager
    {
        private readonly IBrandDal brandDal;

        public BrandManager(IBrandDal _brandDal)
        {
            brandDal = _brandDal;
        }

        public IResult Add(Brand brand)
        {
            this.brandDal.Add(brand);
            return new SuccessResult(Messages.AddSuccess);
        }

        public IResult Update(Brand brand)
        {
            this.brandDal.Update(brand);
            return new SuccessResult(Messages.UpdateSuccess);
        }

        public IResult Delete(Brand brand)
        {
            this.brandDal.Delete(brand);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<Brand> GetBrandById(int id)
        {
            return new SuccessDataResult<Brand>(this.brandDal.Get(c => c.Id == id));
        }

        public int GetCountOfAllBrands()
        {
            return this.brandDal.GetCount();
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.brandDal.IsExists(x => x.Id == id));
        }

        public IDataResult<List<Brand>> GetAllBrands()
        {
            return new SuccessDataResult<List<Brand>>(this.brandDal.GetAll());
        }

        public void WriteAll(List<Brand> brandList)
        {
            foreach (Brand b in brandList)
                Console.WriteLine("ID: #{0}   Name: {1}", b.Id, b.Name);
            Console.WriteLine();
        }
    }
}
