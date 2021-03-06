﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Entities;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await GetProducts(x => x);

            return products.Select(_mapper.Map<Product>);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int take, int skip)
        {
            var products = await GetProducts(x => x.Skip(skip).Take(take));
            
            return products.Select(_mapper.Map<Product>);
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _context.Set<ProductDto>().CountAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await GetProducts(x => x.Where(y => y.ProductId == id));

            return _mapper.Map<Product>(product.FirstOrDefault());
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            _context.Set<ProductDto>().Add(productDto);
            await _context.SaveChangesAsync();

            return _mapper.Map<Product>(productDto);
        }

        public async Task EditProductAsync(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            _context.Set<ProductDto>().Update(productDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = (await GetProducts(x => x.Where(y => y.ProductId == id))).FirstOrDefault();

            if (product == null)
            {
                return;
            }

            _context.Set<ProductDto>().Remove(product);
            await _context.SaveChangesAsync();
        }

        private async Task<IEnumerable<ProductDto>> GetProducts(
            Func<IQueryable<ProductDto>, IQueryable<ProductDto>> filterQuery)
        {
            return await filterQuery(_context.Set<ProductDto>()
                .Include(x => x.Category)
                .Include(x => x.Supplier))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
