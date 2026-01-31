using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IAddressService
{
    // CRUD Operations
    Task<List<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(int id);
    Task<int> CreateAsync(Address address);
    Task<bool> UpdateAsync(int id, Address address);
    Task<bool> DeleteAsync(int id);

    // Custom Business Methods
    Task<List<Address>> GetAddressesByCustomerAsync(int customerId);
    Task<List<Address>> GetAddressesBySupplierAsync(int supplierId);
    Task<List<Address>> SearchAddressesByCityAsync(string city);
    Task<List<Address>> SearchAddressesByDistrictAsync(string district);
    Task<List<Address>> GetAddressesByFiltersAsync(
        string? city = null,
        string? district = null,
        string? country = null,
        int? customerId = null,
        int? supplierId = null);
}