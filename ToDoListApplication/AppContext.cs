using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApplication
{
    public class AppContext : DbContext
    {
        public DbSet<Lists> AllLists { get; set; }
        public DbSet<ToDo> ToDoTask { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database= tododatabase;Trusted_Connection=True;");
        }

    }
}
