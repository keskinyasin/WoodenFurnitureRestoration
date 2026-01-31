using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ICustomerService
{
    // CRUD Operations
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<int> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(int id, Customer customer);
    Task<bool> DeleteAsync(int id);

    // Custom Business Methods
    Task<Customer?> GetByEmailAsync(string email);
    Task<List<Customer>> GetCustomersByCityAsync(string city);
    Task<List<Customer>> GetCustomersByCountryAsync(string country);
    Task<List<Customer>> GetCustomersByAddressAndRestorationAsync(
        string city,
        string district,
        string country,
        int restorationId);
    Task<List<Customer>> GetCustomersByFiltersAsync(
        string? firstName = null,
        string? lastName = null,
        string? email = null,
        string? city = null,
        string? country = null);
    Task<bool> ChangePasswordAsync(int customerId, string currentPassword, string newPassword);
}