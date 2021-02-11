using Core.Utilities.Results;
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
        IDataResult<Brand> GetBrandById(int id);
        int GetCountOfAllBrands();
        IDataResult<List<Brand>> GetAllBrands();
    }
}
