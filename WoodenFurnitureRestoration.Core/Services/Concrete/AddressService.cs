using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class AddressService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Address>(unitOfWork), IAddressService
{
    private readonly IMapper _mapper = mapper;

    protected override IRepository<Address> Repository => _unitOfWork.AddressRepository;

    protected override void ValidateEntity(Address entity)
    {
        if (string.IsNullOrWhiteSpace(entity.AddressLine1))
            throw new ArgumentException("Adres satırı zorunludur.");
        if (string.IsNullOrWhiteSpace(entity.City))
            throw new ArgumentException("Şehir zorunludur.");
        if (string.IsNullOrWhiteSpace(entity.District))
            throw new ArgumentException("İlçe zorunludur.");
    }

    public async Task<List<Address>> GetAddressesByCustomerAsync(int customerId)
    {
        return await Repository.GetAllAsync(a => a.CustomerId == customerId && !a.Deleted);
    }

    public async Task<List<Address>> GetAddressesBySupplierAsync(int supplierId)
    {
        return await Repository.GetAllAsync(a => a.SupplierId == supplierId && !a.Deleted);
    }

    public async Task<List<Address>> SearchAddressesByCityAsync(string city)
    {
        return await Repository.GetAllAsync(a => a.City.Contains(city) && !a.Deleted);
    }

    public async Task<List<Address>> SearchAddressesByDistrictAsync(string district)
    {
        return await Repository.GetAllAsync(a => a.District.Contains(district) && !a.Deleted);
    }

    public async Task<List<Address>> GetAddressesByFiltersAsync(
        string? city = null,
        string? district = null,
        string? country = null,
        int? customerId = null,
        int? supplierId = null)
    {
        var addresses = await Repository.GetAllAsync(a => !a.Deleted);

        if (!string.IsNullOrWhiteSpace(city))
            addresses = addresses.Where(a => a.City.Contains(city, StringComparison.OrdinalIgnoreCase)).ToList();
        if (!string.IsNullOrWhiteSpace(district))
            addresses = addresses.Where(a => a.District.Contains(district, StringComparison.OrdinalIgnoreCase)).ToList();
        if (!string.IsNullOrWhiteSpace(country))
            addresses = addresses.Where(a => a.Country.Contains(country, StringComparison.OrdinalIgnoreCase)).ToList();
        if (customerId.HasValue)
            addresses = addresses.Where(a => a.CustomerId == customerId).ToList();
        if (supplierId.HasValue)
            addresses = addresses.Where(a => a.SupplierId == supplierId).ToList();

        return addresses;
    }
}