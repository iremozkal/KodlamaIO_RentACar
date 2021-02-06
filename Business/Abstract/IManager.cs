using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IManager<T> where T : IEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool IsExistById(int id);
        void WriteAll(List<T> entityList);
    }
}
