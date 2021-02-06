using Business.Abstract;
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

        public void Add(Brand brand)
        {
            this.brandDal.Add(brand);
            Console.WriteLine("(+) Insert operation is succesfully done.");
        }

        public void Update(Brand brand)
        {
            this.brandDal.Update(brand);
            Console.WriteLine("(+) Update operation is succesfully done.");
        }

        public void Delete(Brand brand)
        {
            this.brandDal.Delete(brand);
            Console.WriteLine("(+) Delete operation is succesfully done.");
        }

        public Brand GetBrandById(int id)
        {
            return this.brandDal.Get(c => c.Id == id);
        }

        public int GetCountOfAllBrands()
        {
            return this.brandDal.GetCount();
        }

        public bool IsExistById(int id)
        {
            return this.brandDal.IsExists(x => x.Id == id);
        }

        public List<Brand> GetAllBrands()
        {
            return this.brandDal.GetAll();
        }

        public void WriteAll(List<Brand> brandList)
        {
            foreach (Brand b in brandList)
                Console.WriteLine("ID: #{0}   Name: {1}", b.Id, b.Name);
            Console.WriteLine();
        }
    }
}
