using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorManager : IManager<Color>
    {
        IDataResult<Color> GetColorById(int id);
        int GetCountOfAllColors();
        IDataResult<List<Color>> GetAllColors();
    }
}
