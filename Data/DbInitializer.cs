using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JoshuaWood_ST10296167_PROG7311_POE.Models;

namespace JoshuaWood_ST10296167_PROG7311_POE.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Roles.Any())
                {
                    return;
                }

                // If Db not seeded then create roles
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { "Farmer", "Employee" };

                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Create default users
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                // Default Employee user
                var employeeEmail = "employee@agrienergy.com";
                if (await userManager.FindByEmailAsync(employeeEmail) == null)
                {
                    var employee = new User
                    {
                        UserName = employeeEmail,
                        Email = employeeEmail,
                        FirstName = "Default",
                        LastName = "Employee",
                        EmailConfirmed = true 
                    };

                    var result = await userManager.CreateAsync(employee, "Employee123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(employee, "Employee");
                        Console.WriteLine("Created EMPLOYEE");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description); 
                        }
                    }
                }

                // Default Farmer user
                var farmerEmail = "farmer@agrienergy.com";
                if (await userManager.FindByEmailAsync(farmerEmail) == null)
                {
                    var farmer = new User
                    {
                        UserName = farmerEmail,
                        Email = farmerEmail,
                        FirstName = "Default",
                        LastName = "Farmer",
                        EmailConfirmed = true,
                        FarmerCode = "F001"
                    };

                    var result = await userManager.CreateAsync(farmer, "Farmer123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(farmer, "Farmer");
                        Console.WriteLine("Created FARMER");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description); 
                        }
                    }
                }
            }
        }
    }
}
