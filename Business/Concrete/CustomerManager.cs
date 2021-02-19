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
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal customerDal;

        public CustomerManager(ICustomerDal _customerDal)
        {
            customerDal = _customerDal;
        }

        public IDataResult<Customer> Add(Customer customer)
        {
            this.customerDal.Add(customer);
            return new SuccessDataResult<Customer>(customer, Messages.AddSuccess);
        }

        public IDataResult<Customer> Update(Customer customer)
        {
            this.customerDal.Update(customer);
            return new SuccessDataResult<Customer>(customer, Messages.UpdateSuccess);
        }

        public IDataResult<Customer> Delete(Customer customer)
        {
            this.customerDal.Delete(customer);
            return new SuccessDataResult<Customer>(customer, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.customerDal.IsExists(x => x.UserId == id));
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = this.customerDal.Get(c => c.UserId == id);
            if (result != null)
                return new SuccessDataResult<Customer>(result);
            else
                return new ErrorDataResult<Customer>(result, "NotFound");
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(this.customerDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return this.customerDal.GetCount();
        }

        public IDataResult<CustomerDetailDto> GetCustomerDto(Expression<Func<Customer, bool>> filter = null)
        {
            return new SuccessDataResult<CustomerDetailDto>(this.customerDal.GetCustomerDto(filter).Data);
        }

        public void WriteAll(List<Customer> customerList)
        {
            List<CustomerDetailDto> customerDtoList = new List<CustomerDetailDto>();

            foreach (Customer c in customerList)
            {
                var customerDto = this.customerDal.GetCustomerDto(x => x.UserId == c.UserId).Data;
                customerDtoList.Add(customerDto);
            }

            WriteAllCustomerDetails(customerDtoList);
        }

        public void WriteAllCustomerDetails(List<CustomerDetailDto> customerDtoList)
        {
            foreach (CustomerDetailDto c in customerDtoList)
                Console.WriteLine("ID: #{0,-5}   FirstName: {1,-10}   LastName: {2,-10}   Email: {3,-10}    Company Name: {4}",
                    c.Id, c.FirstName, c.LastName, c.Email, c.CompanyName);
            Console.WriteLine();
        }
    }
}
