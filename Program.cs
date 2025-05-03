using JoshuaWood_ST10296167_PROG7311_POE.Data;
using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Repository.Product;
using JoshuaWood_ST10296167_PROG7311_POE.Repository.User;
using JoshuaWood_ST10296167_PROG7311_POE.Services.Product;
using JoshuaWood_ST10296167_PROG7311_POE.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            // Services and Repos
            builder.Services.AddScoped<IUserService, UserService>(); 
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            var app = builder.Build();

            // Call the database initializer
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                DbInitializer.Initialize(services).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
