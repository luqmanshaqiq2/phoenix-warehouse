using System;

namespace phoenix_warehouse.models
{
    public sealed class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    }
}