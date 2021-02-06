using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandManager
    {
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(Brand brand);
        Brand GetBrandById(int id);
        int GetCountOfAllBrands();
        bool IsExistById(int id);
        List<Brand> GetAllBrands();
        void WriteAll(List<Brand> brandList);
    }
}
