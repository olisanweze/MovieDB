using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MovieDB.DAL;
using MovieDB.BLL;
using MovieDB.Models;

namespace MovieDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //register DbContext
            builder.Services.AddDbContext<MovieDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //add identity service
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MovieDBContext>()
                .AddDefaultUI();

            // Register MoviePlayListBLL and MoviePlayListDAL
            builder.Services.AddScoped<MoviePlayListService>();
            builder.Services.AddScoped<MoviePlayListDAL>();
            builder.Services.AddScoped<PlayListService>();
            builder.Services.AddScoped<PlayListDAL>();
            //register DAL and BLL services
            builder.Services.AddScoped<MovieDAL>();
            builder.Services.AddScoped<MovieService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MoviePlayList}/{action=Create}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}
