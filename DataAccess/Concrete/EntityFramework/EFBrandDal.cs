using CarRent.DataAccess.Abstract;
using CarRent.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.DataAccess.Concrete.EntityFramework
{
    public class EFBrandDal : IBrandDal
    {
        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            using (CarRentContext context = new CarRentContext())
            {
                return context.Set<Brand>().SingleOrDefault(filter);
            }
        }

        public int GetCount()
        {
            using (CarRentContext context = new CarRentContext())
            {
                return context.Set<Brand>().Count();
            }
        }

        public bool IsExists(Expression<Func<Brand, bool>> filter)
        {
            using (CarRentContext context = new CarRentContext())
            {
                return context.Set<Brand>().Any(filter);
            }
        }

        public List<Brand> GetAll(Expression<Func<Brand,bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                return filter == null
                    ? context.Set<Brand>().ToList()
                    : context.Set<Brand>().Where(filter).ToList();
            }
        }

        public void Add(Brand entity)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Brand entity)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Brand entity)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
