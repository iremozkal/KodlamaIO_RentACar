using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFUserDal : EfEntityRepositoryBase<User, CarRentContext>, IUserDal
    {
    }
}
