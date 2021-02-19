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
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;

        public UserManager(IUserDal _userDal)
        {
            userDal = _userDal;
        }

        public IDataResult<User> Add(User user)
        {
            this.userDal.Add(user);
            return new SuccessDataResult<User>(user, Messages.AddSuccess);
        }

        public IDataResult<User> Update(User user)
        {
            this.userDal.Update(user);
            return new SuccessDataResult<User>(user, Messages.UpdateSuccess);
        }

        public IDataResult<User> Delete(User user)
        {
            this.userDal.Delete(user);
            return new SuccessDataResult<User>(user, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(this.userDal.IsExists(x => x.UserId == id));
        }

        public IDataResult<User> GetById(int id)
        {
            var result = this.userDal.Get(c => c.UserId == id);
            if (result != null)
                return new SuccessDataResult<User>(result);
            else
                return new ErrorDataResult<User>(result, "NotFound");
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(this.userDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return this.userDal.GetCount();
        }

        public void WriteAll(List<User> userList)
        {
            foreach (User u in userList)
                Console.WriteLine("ID: #{0,-5}   FirstName: {1,-10}   LastName: {2,-10}   Email: {3}",
                    u.UserId, u.FirstName, u.LastName, u.Email);
            Console.WriteLine();
        }
    }
}
