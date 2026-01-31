using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class AddressService(IUnitOfWork unitOfWork, IMapper mapper) : IAddressService
{
    #region CRUD Operations

    public async Task<List<Address>> GetAllAsync()
    {
        return await unitOfWork.AddressRepository.GetAllAsync(a => !a.Deleted);
    }

    public async Task<Address?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir adres ID'si gereklidir.", nameof(id));

        return await unitOfWork.AddressRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Address address)
    {
        ArgumentNullException.ThrowIfNull(address);
        ValidateAddress(address);

        try
        {
            address.CreatedDate = DateTime.Now;
            address.UpdatedDate = DateTime.Now;
            address.Deleted = false;
            await unitOfWork.AddressRepository.AddAsync(address);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Adres kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Address address)
    {
        ArgumentNullException.ThrowIfNull(address);
        ValidateAddress(address);

        var existing = await unitOfWork.AddressRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(address, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.AddressRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Adres güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var address = await unitOfWork.AddressRepository.FindAsync(id);
        if (address is null) return false;

        try
        {
            // Soft Delete
            address.Deleted = true;
            address.UpdatedDate = DateTime.Now;
            await unitOfWork.AddressRepository.UpdateAsync(address);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Adres silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Address>> GetAddressesByCustomerAsync(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Geçerli bir müşteri ID'si gereklidir.", nameof(customerId));

        return await unitOfWork.AddressRepository.GetAllAsync(a =>
            a.CustomerId == customerId && !a.Deleted);
    }

    public async Task<List<Address>> GetAddressesBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.AddressRepository.GetAllAsync(a =>
            a.SupplierId == supplierId && !a.Deleted);
    }

    public async Task<List<Address>> SearchAddressesByCityAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Şehir adı gereklidir.", nameof(city));

        return await unitOfWork.AddressRepository.GetAllAsync(a =>
            a.City.Contains(city) && !a.Deleted);
    }

    public async Task<List<Address>> SearchAddressesByDistrictAsync(string district)
    {
        if (string.IsNullOrWhiteSpace(district))
            throw new ArgumentException("İlçe adı gereklidir.", nameof(district));

        return await unitOfWork.AddressRepository.GetAllAsync(a =>
            a.District.Contains(district) && !a.Deleted);
    }

    public async Task<List<Address>> GetAddressesByFiltersAsync(
        string? city = null,
        string? district = null,
        string? country = null,
        int? customerId = null,
        int? supplierId = null)
    {
        return await unitOfWork.AddressRepository.GetAllAsync(a =>
            !a.Deleted &&
            (string.IsNullOrEmpty(city) || a.City.Contains(city)) &&
            (string.IsNullOrEmpty(district) || a.District.Contains(district)) &&
            (string.IsNullOrEmpty(country) || a.Country.Contains(country)) &&
            (!customerId.HasValue || a.CustomerId == customerId.Value) &&
            (!supplierId.HasValue || a.SupplierId == supplierId.Value));
    }

    #endregion

    #region Validation

    private static void ValidateAddress(Address address)
    {
        if (string.IsNullOrWhiteSpace(address.AddressLine1))
            throw new ArgumentException("Adres satırı 1 gereklidir.", nameof(address));

        if (string.IsNullOrWhiteSpace(address.City))
            throw new ArgumentException("Şehir gereklidir.", nameof(address));

        if (string.IsNullOrWhiteSpace(address.District))
            throw new ArgumentException("İlçe gereklidir.", nameof(address));

        if (string.IsNullOrWhiteSpace(address.PostalCode))
            throw new ArgumentException("Posta kodu gereklidir.", nameof(address));

        if (string.IsNullOrWhiteSpace(address.Country))
            throw new ArgumentException("Ülke gereklidir.", nameof(address));
    }

    #endregion
}