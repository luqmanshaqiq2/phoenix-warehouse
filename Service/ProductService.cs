using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service
{
    public sealed class ProductService : IProductService
    {
        private readonly WarehouseDbContext _context;

        public ProductService(WarehouseDbContext context) => _context = context;

        public Task<List<Product>> GetAll() => _context.Products.ToListAsync();

        public Task<Product?> GetById(Guid productId) => _context.Products.FindAsync(productId).AsTask();
        
    public async Task<bool> ActivateAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null || product.IsAvailable)
        return false;

        product.IsAvailable = true;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null || !product.IsAvailable)
        return false;

        product.IsAvailable = false;

        await _context.SaveChangesAsync();
        return true;
    }

        public async Task<Product> Create(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }


        public async Task<Product?> Delete(Guid productId)
        {
             var product = await _context.Products.FindAsync(productId);

            if (product is null)
            return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> Update(Guid productId, Product updated)
        {
            ArgumentNullException.ThrowIfNull(updated);

            var product = await _context.Products.FindAsync(productId);

            if (product is null)
            return null;

    
    product.ProductName = updated.ProductName;
    product.SKU = updated.SKU;
    product.Price = updated.Price;
    product.IsAvailable = updated.IsAvailable;
    product.Quantity = updated.Quantity;
    

    await _context.SaveChangesAsync();

    return product;
        }
    }
}