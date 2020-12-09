using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM
{
    public class ApplicationDbContext : DbContext
    {
        readonly DbContextOptions<ApplicationDbContext> options;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.options = options;
        }
        public ApplicationDbContext() { }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RequestUser> RequestUser { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Plane> Plane { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<RequestDelivery> RequestDeliveries { get; set; }
        public DbSet<Country> Country { get; set; }

    }
}
