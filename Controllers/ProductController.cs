using JoshuaWood_ST10296167_PROG7311_POE.Models;
using JoshuaWood_ST10296167_PROG7311_POE.Services.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JoshuaWood_ST10296167_PROG7311_POE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;

        // Controller
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public ProductController(IProductService productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Views
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public async Task<IActionResult> AddProduct()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farmer = await _userManager.FindByIdAsync(userId);

            if (farmer == null || string.IsNullOrEmpty(farmer.FarmerCode))
            {
                TempData["Error"] = "Current farmer not found or missing FarmerCode.";
                return RedirectToAction("Index", "Home");
            }

            var newProductCode = await _productService.GenerateProductCodeAsync();

            var model = new Product
            {
                ProductCode = newProductCode,
                FarmerCode = farmer.FarmerCode
            };

            return View(model);
        }

        public async Task<IActionResult> ViewFarmerProducts()
        {
            var user = await _userManager.GetUserAsync(User);
            var farmerCode = user.FarmerCode;

            var products = await _productService.GetProductsByFarmerAsync(farmerCode);
            return View(products);
        }

        public async Task<IActionResult> ViewProducts(string farmerCode, string category, DateTime? startDate, DateTime? endDate)
        {
            // Populate filter model with selected filters
            var filteredModel = new FilteredProducts
            {
                SelectedFarmerCode = farmerCode,
                SelectedCategory = category,
                StartDate = startDate,
                EndDate = endDate
            };

            // Get list of products based on the filters
            filteredModel.Products = await _productService.GetFilteredProductsAsync(filteredModel);

            // Populate filter options 
            var products = await _productService.GetProductsAsync();
            filteredModel.FarmerCodes = products.Select(p => p.FarmerCode).Distinct().ToList();
            filteredModel.Categories = products.Select(p => p.Category).Distinct().ToList();

            return View(filteredModel);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> AddFarmerProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return View("AddProduct");
            }

            var result = await _productService.AddProductAsync(product);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Failed to add product";
            return View("AddProduct");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//