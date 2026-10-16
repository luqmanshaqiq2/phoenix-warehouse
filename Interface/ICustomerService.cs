using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<Customer?> GetById(Guid customerId);
        Task<Customer> Create(Customer customer);
        Task<Customer?> Update(Guid customerId, Customer customer);
        Task<Customer?> Delete(Guid customerId);
    }
}
