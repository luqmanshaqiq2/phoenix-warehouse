using System;

namespace phoenix_warehouse.models
{
    public sealed class Inventory
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReservedQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;
        public int AvailableQuantity => QuantityOnHand - ReservedQuantity;

        public void AddStock(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Stock addition must be greater than zero.");
            }

            QuantityOnHand += amount;
            LastUpdated = DateTime.UtcNow;
        }

        public bool Reserve(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Reservation amount must be greater than zero.");
            }

            if (AvailableQuantity < amount)
            {
                return false;
            }

            ReservedQuantity += amount;
            LastUpdated = DateTime.UtcNow;
            return true;
        }

        public void ReleaseReservation(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Release amount must be greater than zero.");
            }

            if (amount > ReservedQuantity)
            {
                throw new InvalidOperationException("Cannot release more than the reserved quantity.");
            }

            ReservedQuantity -= amount;
            LastUpdated = DateTime.UtcNow;
        }

        public void Adjust(int delta)
        {
            if (delta == 0)
            {
                return;
            }

            if (QuantityOnHand + delta < 0)
            {
                throw new InvalidOperationException("Inventory cannot go below zero.");
            }

            QuantityOnHand += delta;
            LastUpdated = DateTime.UtcNow;
        }
    }
}