using ExamBuilder.DataAccess.UnitOfWork;
using ExamBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBuilder.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _uow;
        public UserBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<User> GetAll()
        {
            return _uow.Users.GetAll();
        }

        public User Get(int id)
        {
            return _uow.Users.Get(id);
        }

        public User GetBy(string userName, string password)
        {
            var user = _uow.Users.Get(
                user => user.UserName == userName
                && user.Password == password);

            return user;
        }

        public ProcessResult Add(User user)
        {
            ProcessResult result = new ProcessResult();
            _uow.Users.Add(user);
            try
            {
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public ProcessResult Delete(User user)
        {
            ProcessResult result = new ProcessResult();
            _uow.Users.Delete(user);
            try
            {
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public ProcessResult Delete(int id)
        {
            ProcessResult result = new ProcessResult();
            _uow.Users.Delete(id);
            try
            {
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public ProcessResult Update(User user)
        {
            ProcessResult result = new ProcessResult();
            _uow.Users.Update(user);
            try
            {
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
    public interface IUserBusiness
    {
        ProcessResult Add(User user);
        ProcessResult Delete(User user);
        ProcessResult Delete(int id);
        User Get(int id);
        User GetBy(string userName, string password);
        IEnumerable<User> GetAll();
        ProcessResult Update(User user);
    }
}
