using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieDB.DAL;
namespace MovieDB

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MovieDBContextConnection") ?? throw new InvalidOperationException("Connection string 'MovieDBContextConnection' not found.");

            builder.Services.AddDbContext<MovieDBContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MovieDBContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Step 4: add identity service
            //order app.

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=Index}/{Id?}");
            app.MapRazorPages(); // razor pages

            app.Run();
        }
    }
}