using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<User> Add(User user)
        {
            _userDal.Add(user);
            return new SuccessDataResult<User>(user, Messages.AddSuccess);
        }

        public IDataResult<User> Update(User user)
        {
            _userDal.Update(user);
            return new SuccessDataResult<User>(user, Messages.UpdateSuccess);
        }

        public IDataResult<User> Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessDataResult<User>(user, Messages.DeleteSuccess);
        }

        public IResult IsExistById(int id)
        {
            return new Result(_userDal.IsExists(x => x.Id == id));
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(c => c.Id == id);
            if (result != null)
                return new SuccessDataResult<User>(result);
            else
                return new ErrorDataResult<User>(result, Messages.UserNotFound);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public int GetCountOfAll()
        {
            return _userDal.GetCount();
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public void WriteAll(List<User> userList)
        {
            foreach (User u in userList)
                Console.WriteLine("ID: #{0,-5}   FirstName: {1,-10}   LastName: {2,-10}   Email: {3}",
                    u.Id, u.FirstName, u.LastName, u.Email);
            Console.WriteLine();
        }
    }
}
