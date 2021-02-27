using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarImageDal : EfEntityRepositoryBase<CarImage, CarRentContext>, ICarImageDal
    {
    }
}
