﻿using JoshuaWood_ST10296167_PROG7311_POE.Models;
using System.Security.Claims;

namespace JoshuaWood_ST10296167_PROG7311_POE.Services.Product
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(Models.Product product);

        Task<string> GenerateProductCodeAsync();

        Task<List<Models.Product>> GetProductsByFarmerAsync(string farmerCode);

        Task<List<Models.Product>> GetProductsAsync();

        Task<List<Models.Product>> GetFilteredProductsAsync(FilteredProducts filter);
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//