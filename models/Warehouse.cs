using System;
using System.Collections.Generic;

namespace phoenix_warehouse.models
{
    public sealed class Warehouse
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<Inventory> Inventory { get; } = new();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public bool HasCapacity(int additionalItems)
        {
            return additionalItems >= 0 && (Capacity - Inventory.Sum(i => i.QuantityOnHand)) >= additionalItems;
        }
    }
}