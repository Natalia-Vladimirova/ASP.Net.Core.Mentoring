﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> GetProductsAsync(int count);

        Task<Product> GetProductAsync(int id);

        Task AddProductAsync(Product product);

        Task EditProductAsync(Product product);
    }
}
