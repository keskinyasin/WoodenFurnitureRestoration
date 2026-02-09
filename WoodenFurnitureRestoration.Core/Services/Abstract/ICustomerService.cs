using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ICustomerService : IService<Customer>
{
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