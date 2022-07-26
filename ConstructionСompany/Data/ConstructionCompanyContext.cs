using ConstructionСompany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionСompany.Data
{
    public class ConstructionCompanyContext : DbContext
    {
        public ConstructionCompanyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<TypeOfJob> TypeOfJobs { get; set; }
    }
}
