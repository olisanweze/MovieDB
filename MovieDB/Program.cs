using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieDB.BLL;
using MovieDB.DAL;

namespace MovieDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure the connection string
            var connectionString = builder.Configuration.GetConnectionString("MovieDBContextConnection")
                ?? throw new InvalidOperationException("Connection string 'MovieDBContextConnection' not found.");

            // Register DbContext
            builder.Services.AddDbContext<MovieDBContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Identity services
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MovieDBContext>()
                .AddDefaultUI();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            // Register BLL and DAL services
            builder.Services.AddScoped<MoviePlayListService>();
            builder.Services.AddScoped<MoviePlayListDAL>();
            builder.Services.AddScoped<PlayListService>();
            builder.Services.AddScoped<PlayListDAL>();
            builder.Services.AddScoped<MovieDAL>();
            builder.Services.AddScoped<MovieService>();

            var app = builder.Build();

            // Create roles
            CreateRoles(app).Wait();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static async Task CreateRoles(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string[] roleNames = { "Admin", "User" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
                if (adminUser != null)
                {
                    var isInAdminRole = await userManager.IsInRoleAsync(adminUser, "Admin");
                    if (!isInAdminRole)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }
    }
}
