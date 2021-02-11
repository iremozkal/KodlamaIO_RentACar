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
    public class ColorManager : IColorManager
    {
        private readonly IColorDal colorDal;

        public ColorManager(IColorDal _colorDal)
        {
            colorDal = _colorDal;
        }

        public IResult Add(Color color)
        {
            this.colorDal.Add(color);
            return new SuccessResult(Messages.AddSuccess);
        }

        public IResult Update(Color color)
        {
            this.colorDal.Update(color);
            return new SuccessResult(Messages.UpdateSuccess);
        }

        public IResult Delete(Color color)
        {
            this.colorDal.Delete(color);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<Color> GetColorById(int id)
        {
            return new SuccessDataResult<Color>(this.colorDal.Get(c => c.Id == id));
        }

        public int GetCountOfAllColors()
        {
            return this.colorDal.GetCount();
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.colorDal.IsExists(x => x.Id == id));
        }

        public IDataResult<List<Color>> GetAllColors()
        {
            return new SuccessDataResult<List<Color>>(this.colorDal.GetAll());
        }

        public void WriteAll(List<Color> colorList)
        {
            foreach (Color c in colorList)
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}", c.Id, c.Name);
            Console.WriteLine();
        }
    }
}
