using ExamBuilder.DataAccess.UnitOfWork;
using ExamBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBuilder.Business
{
    public class QuestionBusiness : IQuestionBusiness
    {
        private readonly IUnitOfWork _uow;
        public QuestionBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Question> GetAll()
        {
            return _uow.Questions.GetAll();
        }

        public Question Get(int id)
        {
            return _uow.Questions.Get(id);
        }

        public ProcessResult Add(Question question)
        {
            ProcessResult result = new ProcessResult();
            _uow.Questions.Add(question);
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

        public ProcessResult Delete(Question question)
        {
            ProcessResult result = new ProcessResult();
            _uow.Questions.Delete(question);
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
            _uow.Questions.Delete(id);
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

        public ProcessResult Update(Question question)
        {
            ProcessResult result = new ProcessResult();
            _uow.Questions.Update(question);
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
    public interface IQuestionBusiness
    {
        ProcessResult Add(Question question);
        ProcessResult Delete(Question question);
        ProcessResult Delete(int id);
        Question Get(int id);
        IEnumerable<Question> GetAll();
        ProcessResult Update(Question question);
    }
}
