using DependencyStore.Models;

namespace DependencyStore.Repositories.Contracts;

public interface ICustomerRepository
{
    public Task<Customer?> GetByIdAsync(string customerId);
}
