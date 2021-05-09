using AviaTM.Db.Models;
using AviaTM.DB.Model.Models;
using Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AviaTM
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        //readonly DbContextOptions<ApplicationDbContext> options;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        public ApplicationDbContext() { }
        //public DbSet<Provider> Provider { get; set; }
        //public DbSet<User> User { get; set; }
        //public DbSet<RequestUser> RequestUser { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Plane> Plane { get; set; }
        //public DbSet<Order> Order { get; set; }
        //public DbSet<Cargo> Cargo { get; set; }
        public DbSet<RequestDelivery> RequestDeliveries { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Cargo> Cargoes{ get; set; }
        public DbSet<InfoTransfer> InfoTransfers { get; set; }
        public DbSet<OrderData> OrderDats{ get; set; }
        public DbSet<OrderMain> OrderMains{ get; set; }
        public DbSet<RegisterViewModel> Registers{ get; set; }
        public DbSet<RouteMap> RouteMaps{ get; set; }
        public DbSet<TypeCargo> TypeCargoes{ get; set; }
        public DbSet<TypeTransport> TypeTransports{ get; set; }
        public DbSet<TypeUser> TypeUsers{ get; set; }
        public DbSet<Transport> Transports{ get; set; }


    }
}
