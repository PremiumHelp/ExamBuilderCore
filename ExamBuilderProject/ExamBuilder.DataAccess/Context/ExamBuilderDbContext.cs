using ExamBuilder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExamBuilder.DataAccess.Context
{
    public class ExamBuilderDbContext : DbContext, IExamBuilderDbContext
    {
        //public ExamBuilderDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }

        //public override DatabaseFacade Database { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite($"Data Source = {GetDirectory()}\\ExamBuilder.db");
        }

        private string GetDirectory()
        {
            var layerName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var directories = Directory.GetCurrentDirectory().Split('\\');
            directories.SetValue(layerName, directories.Length - 1);
            return string.Join('\\', directories);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(item.ClrType).ToTable(item.ClrType.Name);
            }
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    (entry.Entity as IEntity).CreatedDate = DateTime.Now;
                    (entry.Entity as IEntity).ModifiedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    (entry.Entity as IEntity).ModifiedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
