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
        Color GetColorById(int id);
        int GetCountOfAllColors();
        List<Color> GetAllColors();
    }
}
