using ExamBuilder.DataAccess.Context;
using ExamBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExamBuilder.DataAccess.Repositories
{
    public class QuestionRepository : IQuestionRepository, IDisposable
    {
        private readonly IExamBuilderDbContext _context;
        private DbSet<Question> _dbSet;
        public QuestionRepository(IExamBuilderDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Question>();
        }

        public IEnumerable<Question> GetAll()
        {
            return _dbSet.ToList();
        }
        public Question Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(Question entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Question entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Question entity)
        {
            entity.IsActive = false;
            Update(entity);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity == null)
                return;

            entity.IsActive = false;
            Update(entity);
        }

        public Question Get(Expression<Func<Question, bool>> predicate)
        {
            return _dbSet.Find(predicate);
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

    public interface IQuestionRepository
    {
        void Add(Question entity);
        void Delete(Question entity);
        void Delete(int id);
        Question Get(Expression<Func<Question, bool>> predicate);
        Question Get(int id);
        IEnumerable<Question> GetAll();
        void Update(Question entity);
        void Dispose();
    }
}
