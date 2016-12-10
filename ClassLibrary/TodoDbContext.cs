using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Classes;

namespace ClassLibrary
{
    public class TodoDbContext : DbContext
    {

        public IDbSet<TodoItem> TodoItems { get; set; }


        public TodoDbContext(string connectionString) : base(connectionString)
        {

        }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().HasKey(s => s.Id);
            modelBuilder.Entity<TodoItem>().Property(s => s.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.DateCompleted).IsOptional();

        }

    }
}
