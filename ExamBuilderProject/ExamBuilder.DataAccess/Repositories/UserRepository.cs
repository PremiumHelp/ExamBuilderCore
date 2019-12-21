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
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly IExamBuilderDbContext _context;
        private DbSet<User> _dbSet;
        public UserRepository(IExamBuilderDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbSet.ToList();
        }
        public User Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(User entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(User entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(User entity)
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

        public User Get(Expression<Func<User, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
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

    public interface IUserRepository
    {
        void Add(User entity);
        void Delete(User entity);
        void Delete(int Id);
        User Get(Expression<Func<User, bool>> predicate);
        User Get(int id);
        IEnumerable<User> GetAll();
        void Update(User entity);
        void Dispose();
    }
}
