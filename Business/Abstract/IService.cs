using Core.Entities;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IService<T> where T : IEntity
    {
        IDataResult<T> Add(T entity);
        IDataResult<T> Update(T entity);
        IDataResult<T> Delete(T entity);
        IResult IsExistById(int id);
        IDataResult<T> GetById(int id);
        IDataResult<List<T>> GetAll();
        int GetCountOfAll();
        void WriteAll(List<T> entityList);
    }
}
