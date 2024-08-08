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
            builder.Services.AddHttpClient();

            //register DbContext
            builder.Services.AddDbContext<MovieDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //add identity service
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
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

            // Create roles
            CreateRoles(app);

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movie}/{action=Create}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        private static void CreateRoles(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string[] roleNames = { "Admin", "User" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = roleManager.RoleExistsAsync(roleName);
                    var roleExistResult = roleExist.Result;
                    if (!roleExistResult)
                    {
                        var roleResult = roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                        if (!roleResult.Succeeded)
                        {
                            // Log or handle the error if role creation fails
                            Console.WriteLine($"Failed to create role: {roleName}");
                        }
                    }
                }

                var adminUser = userManager.FindByEmailAsync("admin@admin.com");
                var adminUserResult = adminUser.Result;

                if (adminUserResult != null)
                {
                    var isInAdminRole = userManager.IsInRoleAsync(adminUserResult, "Admin");
                    var isInAdminRoleResult = isInAdminRole.Result;
                    if (!isInAdminRoleResult)
                    {
                        var result = userManager.AddToRoleAsync(adminUserResult, "Admin").Result;
                    }
                }

                var users = userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = userManager.GetRolesAsync(user).Result;
                    if (!roles.Contains("Admin") && !roles.Contains("User"))
                    {
                        userManager.AddToRoleAsync(user, "User").Wait();
                    }
                }
            }
        }


    }
}
