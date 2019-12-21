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
    public class OptionRepository : IOptionRepository, IDisposable
    {
        private readonly IExamBuilderDbContext _context;
        private DbSet<Option> _dbSet;
        public OptionRepository(IExamBuilderDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Option>();
        }

        public IEnumerable<Option> GetAll()
        {
            return _dbSet.ToList();
        }
        public Option Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(Option entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Option entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Option entity)
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

        public Option Get(Expression<Func<Option, bool>> predicate)
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

    public interface IOptionRepository
    {
        void Add(Option entity);
        void Delete(Option entity);
        void Delete(int id);
        Option Get(Expression<Func<Option, bool>> predicate);
        Option Get(int id);
        IEnumerable<Option> GetAll();
        void Update(Option entity);
        void Dispose();
    }
}
