using ExamBuilder.DataAccess.UnitOfWork;
using ExamBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBuilder.Business
{
    public class ExamBusiness : IExamBusiness
    {
        private readonly IUnitOfWork _uow;
        public ExamBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Exam> GetAll()
        {
            return _uow.Exams.GetAll();
        }

        public Exam Get(int id)
        {
            return _uow.Exams.Get(id);
        }

        public ProcessResult Add(Exam exam)
        {
            ProcessResult result = new ProcessResult();
            _uow.Exams.Add(exam);
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

        public ProcessResult Delete(Exam exam)
        {
            ProcessResult result = new ProcessResult();
            _uow.Exams.Delete(exam);
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
            _uow.Exams.Delete(id);
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

        public ProcessResult Update(Exam exam)
        {
            ProcessResult result = new ProcessResult();
            _uow.Exams.Update(exam);
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
    public interface IExamBusiness
    {
        ProcessResult Add(Exam exam);
        ProcessResult Delete(Exam exam);
        ProcessResult Delete(int id);
        Exam Get(int id);
        IEnumerable<Exam> GetAll();
        ProcessResult Update(Exam exam);
    }
}
