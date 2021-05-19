using AviaTM.DB.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace AviaTM
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        public ApplicationDbContext() { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Cargo> Cargoes{ get; set; }
        public DbSet<OrderData> OrderData { get; set; }
        public DbSet<OrderMain> OrderMain { get; set; }
        public DbSet<RequestMain> RequestMain { get; set; }
        public DbSet<RouteMap> RouteMaps{ get; set; }
        public DbSet<TypeCargo> TypeCargoes{ get; set; }
        public DbSet<TypePayment> TypePayment{ get; set; }
        public DbSet<TypeCurrency> TypeCurrency{ get; set; }
        public DbSet<TypeTransport> TypeTransports{ get; set; }
        public DbSet<TransportLoadCapacity> TransportLoadCapacity { get; set; }
        public DbSet<Transport> Transports{ get; set; }


    }
}
