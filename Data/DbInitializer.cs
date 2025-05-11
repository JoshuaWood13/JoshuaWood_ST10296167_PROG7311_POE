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
                if (context.Roles.Any() && context.Products.Any())
                {
                    return;
                }

                if (!context.Roles.Any())
                {
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    string[] roleNames = { "Farmer", "Employee" };

                    foreach (var roleName in roleNames)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
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

                // Create demonstration farmers
                var farmers = new[]
                {
                    new { Email = "farmer1@agrienergy.com", FarmerCode = "F001", FirstName = "Bob", LastName = "Dylan" },
                    new { Email = "farmer2@agrienergy.com", FarmerCode = "F002", FirstName = "David", LastName = "Bowie" },
                    new { Email = "farmer3@agrienergy.com", FarmerCode = "F003", FirstName = "Anita", LastName = "Baker" }
                };

                foreach (var farmer in farmers)
                {
                    if (await userManager.FindByEmailAsync(farmer.Email) == null)
                    {
                        var newFarmer = new User
                        {
                            UserName = farmer.Email,
                            Email = farmer.Email,
                            FirstName = farmer.FirstName,
                            LastName = farmer.LastName,
                            EmailConfirmed = true,
                            FarmerCode = farmer.FarmerCode
                        };

                        string farmerNumber = farmer.FarmerCode.Substring(1);
                        string password = $"Farmer{int.Parse(farmerNumber)}!";

                        var result = await userManager.CreateAsync(newFarmer, password);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newFarmer, "Farmer");
                            Console.WriteLine($"Created FARMER {farmer.FarmerCode}");
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

                // Add demonstration products
                if (!context.Products.Any())
                {
                    var products = new List<Product>
                    {
                        new Product
                        {
                            ProductCode = "P001",
                            FarmerCode = "F001",
                            Name = "Barley",
                            Price = 20.00m,
                            Description = "Sold per 1kg",
                            Category = "Grains",
                            DateAdded = DateTime.Now.AddDays(-8)

                        },
                        new Product
                        {
                            ProductCode = "P002",
                            FarmerCode = "F002",
                            Name = "Orange",
                            Price = 4.99m,
                            Description = "Sold individually",
                            Category = "Fruits",
                            DateAdded = DateTime.Now.AddDays(-8)

                        },
                        new Product
                        {
                            ProductCode = "P003",
                            FarmerCode = "F001",
                            Name = "Potato",
                            Price = 9.99m,
                            Description = "Sold per 10kg",
                            Category = "Vegetables",
                            DateAdded = DateTime.Now.AddDays(-6)

                        },
                        new Product
                        {
                            ProductCode = "P004",
                            FarmerCode = "F003",
                            Name = "Milk",
                            Price = 10.00m,
                            Description = "Fresh from our pasture-raised cows, this milk is rich, creamy," +
                            " and packed with natural goodness. We milk daily to ensure top quality and deliver straight" +
                            " from our farm to your table.",
                            Category = "Consumer Products",
                            DateAdded = DateTime.Now.AddDays(-5)

                        },
                        new Product
                        {
                            ProductCode = "P005",
                            FarmerCode = "F003",
                            Name = "Cattle",
                            Price = 11500.00m,
                            Description = "The following specifications outline the key qualities and health standards of the cattle offered for sale:" + Environment.NewLine +
                            "- Average age: 2–3 years" + Environment.NewLine + 
                            "- Vaccinated and regularly checked by a licensed vet" + Environment.NewLine +
                            "- Healthy, well-fed cattle raised on natural pasture",
                            Category = "Livestock",
                            DateAdded = DateTime.Now.AddDays(-5)

                        },
                        new Product
                        {
                            ProductCode = "P006",
                            FarmerCode = "F001",
                            Name = "Chicken",
                            Price = 200.00m,
                            Description = "The chickens available for sale have been raised under controlled, healthy conditions to ensure optimal quality and suitability for various purposes:" + Environment.NewLine +
                            "- Average weight: 1.8 – 2.5 kg" + Environment.NewLine +
                            "- Regularly monitored by certified veterinarians" + Environment.NewLine +
                            "- Free-range and fed a balanced, nutrient-rich diet",
                            Category = "Livestock",
                            DateAdded = DateTime.Now.AddDays(-2)

                        },
                        new Product
                        {
                            ProductCode = "P007",
                            FarmerCode = "F002",
                            Name = "Rice",
                            Price = 70.00m,
                            Description = "Sold per 1kg",
                            Category = "Grains",
                            DateAdded = DateTime.Now.AddDays(-1)

                        },
                        new Product
                        {
                            ProductCode = "P008",
                            FarmerCode = "F002",
                            Name = "Carrot",
                            Price = 20.00m,
                            Description = "Sold per 1kg",
                            Category = "Vegetables",
                            DateAdded = DateTime.Now.AddDays(-1)

                        },
                        new Product
                        {
                            ProductCode = "P009",
                            FarmerCode = "F001",
                            Name = "Wine",
                            Price = 399.99m,
                            Description = "Crafted from handpicked grapes and aged to perfection, " +
                            "this wine offers a rich, full-bodied flavor ideal for both casual enjoyment and fine dining.",
                            Category = "Consumer Products",
                            DateAdded = DateTime.Now

                        },
                    };

                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Created demonstration products");
                }
            }
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//