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
    public class ColorManager: IColorManager
    {
        private readonly IColorDal colorDal;

        public ColorManager(IColorDal _colorDal)
        {
            colorDal = _colorDal;
        }

        public void Add(Color color)
        {
            this.colorDal.Add(color);
            Console.WriteLine("(+) Insert operation is succesfully done.");
        }

        public void Update(Color color)
        {
            this.colorDal.Update(color);
            Console.WriteLine("(+) Update operation is succesfully done.");
        }

        public void Delete(Color color)
        {
            this.colorDal.Delete(color);
            Console.WriteLine("(+) Delete operation is succesfully done.");
        }

        public Color GetColorById(int id)
        {
            return this.colorDal.Get(c => c.Id == id);
        }

        public int GetCountOfAllColors()
        {
            return this.colorDal.GetCount();
        }

        public bool IsExistById(int id)
        {
            return this.colorDal.IsExists(x => x.Id == id);
        }

        public List<Color> GetAllColors()
        {
            return this.colorDal.GetAll();
        }

        public void WriteAll(List<Color> colorList)
        {
            foreach (Color c in colorList)
                Console.WriteLine("ID: #{0}   Name: {1}", c.Id, c.Name);
            Console.WriteLine();
        }
    }
}
