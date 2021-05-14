using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using AviaTM.Middlewares;
using AviaTM.Services.Models.Models;
using AviaTM.Services.Models.Infastructure;
using AviaTM.Services.IServicesController;
using AviaTm.Services.Api.ServicesController;
using AviaTM.DB.Model.Models;

namespace AviaTM
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddIdentity<AppUser,IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>();
            

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    assembly =>assembly.MigrationsAssembly("AviaTM"));
            });
            services.Configure<AuthorizationSettings>(Configuration.GetSection("AuthorizationSettings"));

            services.AddControllers();
            services.AddCors();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            RegisterDependencies(services);
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            
            app.UseRouting();   

            app.UseCors(options => options
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            );
            app.UseMiddleware<AuthentificationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            CreateDefaultUserIfNotExist(userManager,roleManager);
        }

        private void CreateDefaultUserIfNotExist(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string name = "Admin";
            const string role = "Admin";
            const string password = "Pa$$w0rd123";
            var roleUser = roleManager.FindByNameAsync(role).Result;

            if (roleUser == null)
            {
                roleUser = new IdentityRole
                {
                    Name = name,
                    NormalizedName = name.ToUpper()
                };
                Task.Run(async () => await roleManager.CreateAsync(roleUser)).Wait();
            }

            var user = userManager.FindByNameAsync(name).Result;

            if (user == null) 
            {
                user = new AppUser
                {
                    Name = name,
                    UserName = name,
                    Email = "admin@gmail.com",
                    PhoneNumber ="+375299093091"
                };

                Task.Run(async () => await userManager.CreateAsync(user, password)).Wait();
                Task.Run(async () => await userManager.AddToRoleAsync(user, name)).Wait();
            }
            //Task.Run(async () => await userManager.AddToRoleAsync(user, role)).Wait();


        }
        private static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<UserContext>();
            services.AddScoped<IRecOrdControllerService, RecOrdControllerService>();
            services.AddScoped<IUserControllerService, UserControllerService>();
            services.AddScoped<ITypePaymentControllerService, TypePaymentControllerService>();
            services.AddScoped<ITypeCurrencyControllerService, TypeCurrencyControllerService>();
            services.AddScoped<ITransportLoadCapacityControllerService, TransportLoadCapacityControllerService>();
            services.AddScoped<ITypeTransportControllerService, TypeTransportControllerService>();
            services.AddScoped<ITransportControllerService,TransportControllerService>();

            services.AddScoped<ITypeCargoControllerService, TypeCargoControllerService>();
            services.AddScoped<ICargoControllerService, CargoControllerService>();
            services.AddScoped<IRouteMapControllerService, RouteMapControllerService>();
            services.AddScoped<IAccountControllerService, AccountControllerService>();
        }
    }
}
