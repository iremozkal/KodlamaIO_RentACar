using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<Rental> Add(Rental rental)
        {
            _rentalDal.Add(rental);
            return new SuccessDataResult<Rental>(rental, Messages.AddSuccess);
        }

        public IDataResult<Rental> Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessDataResult<Rental>(rental, Messages.UpdateSuccess);
        }

        public IDataResult<Rental> Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessDataResult<Rental>(rental, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(_rentalDal.IsExists(x => x.Id == id));
        }

        public IDataResult<Rental> GetById(int id)
        {
            var result = _rentalDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<Rental>(result);
            else
                return new ErrorDataResult<Rental>(result, Messages.RentalNotFound);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return _rentalDal.GetCount();
        }

        public IResult IsReturn(int id)
        {
            var result = _rentalDal.GetRentalDetails(x => x.CarId == id && x.ReturnDate == null).Data;

            if (result == null) return new SuccessResult(Messages.CarIsReturn);
            else return new ErrorResult(Messages.CarNotReturn);
        }

        public IDataResult<RentalDetailDto> GetRentalDto(Expression<Func<Rental, bool>> filter = null)
        {
            return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalDto(filter).Data);
        }

        public void WriteAll(List<Rental> rentalList)
        {
            List<RentalDetailDto> rentalDtoList = new List<RentalDetailDto>();

            foreach (Rental r in rentalList)
            {
                var rentalDto = _rentalDal.GetRentalDto(x => x.Id == r.Id).Data;
                rentalDtoList.Add(rentalDto);
            }

            WriteAllRentalDetails(rentalDtoList);
        }

        public void WriteAllRentalDetails(List<RentalDetailDto> rentalDtoList)
        {
            foreach (RentalDetailDto r in rentalDtoList)
                Console.WriteLine("Rental No: #{0,-3}   Customer: #{1}- {2,-15}  Car: #{3}- {4,-5}   Rent Date: {5,-10}   Return Date: {6}",
                   r.RentalNo, r.CustomerId, r.CustomerName, r.CarId, r.CarName, r.RentDate.ToShortDateString(),
                   r.ReturnDate.HasValue ? r.ReturnDate.Value.ToShortDateString() : "Not return yet.");
            Console.WriteLine();
        }
    }
}
