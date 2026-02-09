using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface IAddressService: IService<Address>
{
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