using Company.MVC.Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.DAL.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // To use all Model Configuration Classes
            // Check EF codes to know more
            base.OnModelCreating(modelBuilder);
        }

        // Look into Program.cs
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CompanyMvcDemo; Trusted_Connection = True; TrustServerCertificate = True");
        //}

        public DbSet<Department> Departments { get; set; }
    }
}
