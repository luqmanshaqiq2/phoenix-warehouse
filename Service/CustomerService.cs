using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service
{
    public sealed class CustomerService : ICustomerService
    {
        private readonly WarehouseDbContext _context;

        public CustomerService(WarehouseDbContext context) => _context = context;

        public Task<List<Customer>> GetAll() => _context.Customers.ToListAsync();

        public Task<Customer?> GetById(Guid customerId) => _context.Customers.FindAsync(customerId).AsTask();

        public async Task<Customer> Create(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> Update(Guid customerId, Customer updated)
        {
            ArgumentNullException.ThrowIfNull(updated);

            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is null)
            {
                return null;
            }

            customer.FirstName = updated.FirstName;
            customer.LastName = updated.LastName;
            customer.Email = updated.Email;
            customer.PhoneNumber = updated.PhoneNumber;

            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> Delete(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is null)
            {
                return null;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}