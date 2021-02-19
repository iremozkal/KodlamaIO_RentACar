using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal colorDal;

        public ColorManager(IColorDal _colorDal)
        {
            colorDal = _colorDal;
        }

        public IDataResult<Color> Add(Color color)
        {
            this.colorDal.Add(color);
            return new SuccessDataResult<Color>(color, Messages.AddSuccess);
        }

        public IDataResult<Color> Update(Color color)
        {
            this.colorDal.Update(color);
            return new SuccessDataResult<Color>(color, Messages.UpdateSuccess);
        }

        public IDataResult<Color> Delete(Color color)
        {
            this.colorDal.Delete(color);
            return new SuccessDataResult<Color>(color, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.colorDal.IsExists(x => x.Id == id));
        }

        public IDataResult<Color> GetById(int id)
        {
            var result = this.colorDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Color>(result);
            else
                return new ErrorDataResult<Color>(result, "NotFound");
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(this.colorDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return this.colorDal.GetCount();
        }

        public void WriteAll(List<Color> colorList)
        {
            foreach (Color c in colorList)
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}", c.Id, c.Name);
            Console.WriteLine();
        }
    }
}
