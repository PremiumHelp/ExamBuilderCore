using ExamBuilder.DataAccess.Context;
using ExamBuilder.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBuilder.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IExamBuilderDbContext _context;
        public UnitOfWork(IExamBuilderDbContext context,
            IUserRepository users,
            IOptionRepository options,
            IExamRepository exams,
            IQuestionRepository questions)
        {
            _context = context;
            Exams = exams;
            Options = options;
            Questions = questions;
            Users = users;
        }
        public IExamRepository Exams { get; set; }
        public IOptionRepository Options { get; set; }
        public IQuestionRepository Questions { get; set; }
        public IUserRepository Users { get; set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IUnitOfWork
    {
        IExamRepository Exams { get; }
        IOptionRepository Options { get; }
        IQuestionRepository Questions { get; }
        IUserRepository Users { get; }

        void Dispose();
        int SaveChanges();
    }
}
