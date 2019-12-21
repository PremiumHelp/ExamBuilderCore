using ExamBuilder.DataAccess.Context;
using ExamBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExamBuilder.DataAccess.Repositories
{
    public class ExamRepository : IExamRepository, IDisposable
    {
        private readonly IExamBuilderDbContext _context;
        private DbSet<Exam> _dbSet;
        public ExamRepository(IExamBuilderDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Exam>();
        }

        public IEnumerable<Exam> GetAll()
        {
            return _dbSet.ToList();
        }
        public Exam Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(Exam entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Exam entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Exam entity)
        {
            entity.IsActive = false;
            this.Update(entity);
        }
        public Exam Get(Expression<Func<Exam, bool>> predicate)
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

    public interface IExamRepository
    {
        void Add(Exam entity);
        void Delete(Exam entity);
        Exam Get(Expression<Func<Exam, bool>> predicate);
        Exam Get(int id);
        IEnumerable<Exam> GetAll();
        void Update(Exam entity);
        void Dispose();
    }
}
