using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFRentalDal : EfEntityRepositoryBase<Rental, CarRentContext>, IRentalDal
    {
        public IDataResult<RentalDetailDto> GetRentalDto(Expression<Func<Rental, bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var result = from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join car in context.Cars
                             on rental.CarId equals car.Id
                             join customer in context.Customers
                             on rental.CustomerId equals customer.UserId
                             join user in context.Users
                             on customer.UserId equals user.Id
                             select new RentalDetailDto
                             {
                                 RentalNo = rental.Id,
                                 CustomerId = customer.UserId,
                                 CustomerName = user.FirstName + " " + user.LastName,
                                 CarId = car.Id,
                                 CarName = car.Description,
                                 RentDate = rental.RentDate.ToShortDateString(),
                                 ReturnDate = rental.ReturnDate.Value.ToShortDateString()
                             };

                return new SuccessDataResult<RentalDetailDto>(result.FirstOrDefault());
            }
        }

        public IDataResult<List<RentalDetailDto>> GetAllRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (CarRentContext context = new CarRentContext())
            {
                var result = from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                             join car in context.Cars
                             on rental.CarId equals car.Id
                             join customer in context.Customers
                             on rental.CustomerId equals customer.UserId
                             join user in context.Users
                             on customer.UserId equals user.Id
                             select new RentalDetailDto
                             {
                                 RentalNo = rental.Id,
                                 CustomerId = customer.UserId,
                                 CustomerName = user.FirstName + " " + user.LastName,
                                 CarId = car.Id,
                                 CarName = car.Description,
                                 RentDate = rental.RentDate.ToShortDateString(),
                                 ReturnDate = rental.ReturnDate.Value.ToShortDateString()
                             };

               return new SuccessDataResult<List<RentalDetailDto>>(result.ToList());
            }
        }
    }
}
