using ExamBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ExamBuilder.DataAccess.Context
{
    public interface IExamBuilderDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Exam> Exams { get; set; }
        DbSet<Option> Options { get; set; }
        DbSet<Question> Questions { get; set; }
        //DatabaseFacade Database { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
        void Dispose();
    }
}