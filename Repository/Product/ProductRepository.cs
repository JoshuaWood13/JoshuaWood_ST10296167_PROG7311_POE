using JoshuaWood_ST10296167_PROG7311_POE.Data;
using JoshuaWood_ST10296167_PROG7311_POE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoshuaWood_ST10296167_PROG7311_POE.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        // Controller 
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public ProductRepository(ApplicationDbContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<bool> createProduct(Models.Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetLatestProductCodeAsync()
        {
            return await _context.Products
                .OrderByDescending(p => p.ProductCode)
                .Select(p => p.ProductCode)
                .FirstOrDefaultAsync();
        }
    }
}
