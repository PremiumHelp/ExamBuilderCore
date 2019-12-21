using ExamBuilder.DataAccess.UnitOfWork;
using ExamBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBuilder.Business
{
    public class OptionBusiness : IOptionBusiness
    {
        private readonly IUnitOfWork _uow;
        public OptionBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Option> GetAll()
        {
            return _uow.Options.GetAll();
        }

        public Option Get(int id)
        {
            return _uow.Options.Get(id);
        }

        public ProcessResult Add(Option option)
        {
            ProcessResult result = new ProcessResult();
            _uow.Options.Add(option);
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

        public ProcessResult Delete(Option option)
        {
            ProcessResult result = new ProcessResult();
            _uow.Options.Delete(option);
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

        public ProcessResult Update(Option option)
        {
            ProcessResult result = new ProcessResult();
            _uow.Options.Update(option);
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
    public interface IOptionBusiness
    {
        ProcessResult Add(Option option);
        ProcessResult Delete(Option option);
        Option Get(int id);
        IEnumerable<Option> GetAll();
        ProcessResult Update(Option option);
    }
}
