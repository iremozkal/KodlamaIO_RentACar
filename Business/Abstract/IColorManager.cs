using CarRent.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Business.Abstract
{
    public interface IColorManager
    {
        void Add(Color color);
        void Update(Color color);
        void Delete(Color color);
        Color GetColorById(int id);
        int GetCountOfAllColors();
        bool IsExistById(int id);
        List<Color> GetAllColors();
        void WriteAll(List<Color> colorList);
    }
}
