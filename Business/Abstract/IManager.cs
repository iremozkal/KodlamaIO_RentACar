using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IManager<T> where T : IEntity
    {
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
        IResult IsExistById(int id);
        void WriteAll(List<T> entityList);
    }
}
