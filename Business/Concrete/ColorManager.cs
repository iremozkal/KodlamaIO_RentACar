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
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<Color> Add(Color color)
        {
           _colorDal.Add(color);
            return new SuccessDataResult<Color>(color, Messages.AddSuccess);
        }

        public IDataResult<Color> Update(Color color)
        {
           _colorDal.Update(color);
            return new SuccessDataResult<Color>(color, Messages.UpdateSuccess);
        }

        public IDataResult<Color> Delete(Color color)
        {
           _colorDal.Delete(color);
            return new SuccessDataResult<Color>(color, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(_colorDal.IsExists(x => x.Id == id));
        }

        public IDataResult<Color> GetById(int id)
        {
            var result = _colorDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Color>(result);
            else
                return new ErrorDataResult<Color>(result, Messages.ColorNotExist);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return _colorDal.GetCount();
        }

        public void WriteAll(List<Color> colorList)
        {
            foreach (Color c in colorList)
                Console.WriteLine("ID: #{0,-5}   Name: {1,-10}", c.Id, c.Name);
            Console.WriteLine();
        }
    }
}
