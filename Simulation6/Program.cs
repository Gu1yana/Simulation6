using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Simulation6.DataAccessLayer;
using Simulation6.Models.LoginRegister;

namespace Simulation6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DewiDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));

            });

            builder.Services.AddIdentityCore<AppUser>(x =>
            {
                x.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz-_";
                x.User.RequireUniqueEmail = true;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequiredLength = 8;
                x.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<DewiDbContext>().AddDefaultTokenProviders();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
