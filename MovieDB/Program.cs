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
            CreateRoles(app).Wait();

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        private static async Task CreateRoles(IHost app)
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