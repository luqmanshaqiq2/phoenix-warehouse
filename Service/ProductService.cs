using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service
{
    public sealed class ProductService : IProductService
    {
        public Task<Product> Activate(Guid productId) // IsAvailable = true;
        {
            
        }

        public Task<Product> Create(Product product)
        {
           
        }

        public Task<Product> Deactivate(Guid productId) // IsAvailable = false;
        {
           
        }

        public Task<Product> Delete(Guid productId)
        {
            
        }

        public Task<Product> Update(Guid productId, Product product)
        {
            
        }
    }
}