using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MIHA09\SQLEXPRESS;Database=WADT;Trusted_Connection=False;User ID=sa;Password=sa;");
        }

        public DbSet<Test> Test { get; set; } 

        public DbSet<Theory> Theory { get; set; }

        public DbSet<Quest> Quests { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }
   
}