using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PMS.Core.Models;
using PMS.Core.Models.DTO;

namespace UnitOfWorkDemo.Infrastructure
{
    public class DbContextClass :DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<EmployeeRecord> EmployeeRecord { get; set; }
        public DbSet<GetEmployeeStatisticsDto> GetEmployeeStatisticsDto { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration for GetEmployeeStatisticsDto
            modelBuilder.Entity<GetEmployeeStatisticsDto>().HasNoKey();

            // Additional configurations for other entities if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
