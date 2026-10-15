using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface
{
    public interface IProductService
    {
        Task<Product> Create (Product product);
        Task<Product?> Update (Guid productId, Product product);
        Task<Product?> Delete (Guid productId);
        Task<bool> DeactivateAsync (Guid productId);
        Task<bool> ActivateAsync(Guid productId);
        
    }
}