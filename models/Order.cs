using System;
using System.Collections.Generic;
using System.Linq;

namespace phoenix_warehouse.models
{
    public sealed class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> Items { get; } = new();

        public void AddItem(Product product, int quantity)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Order quantity must be greater than zero.");
            }

            var item = new OrderItem
            {
                OrderId = Id,
                ProductId = product.Id,
                ProductName = product.ProductName,
                Quantity = quantity,
                UnitPrice = product.Price
            };

            Items.Add(item);
            TotalAmount = Items.Sum(i => i.LineTotal);
        }

        public void UpdateStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException("Status is required.", nameof(status));
            }

            Status = status;
        }
    }
}