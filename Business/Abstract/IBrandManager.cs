using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandManager : IManager<Brand>
    {
        Brand GetBrandById(int id);
        int GetCountOfAllBrands();
        List<Brand> GetAllBrands();
    }
}
