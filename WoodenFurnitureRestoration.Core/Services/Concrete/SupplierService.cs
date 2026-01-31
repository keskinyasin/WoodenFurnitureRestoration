using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class SupplierService(IUnitOfWork unitOfWork, IMapper mapper) : ISupplierService
{
    #region CRUD Operations

    public async Task<List<Supplier>> GetAllAsync()
    {
        return await unitOfWork.SupplierRepository.GetAllAsync(s => !s.Deleted);
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(id));

        return await unitOfWork.SupplierRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);
        ValidateSupplier(supplier);

        // Email benzersizlik kontrolü
        if (!string.IsNullOrWhiteSpace(supplier.SupplierEmail))
        {
            var existingSupplier = await GetSupplierByEmailAsync(supplier.SupplierEmail);
            if (existingSupplier is not null)
                throw new InvalidOperationException("Bu e-posta adresi zaten kayıtlı.");
        }

        try
        {
            supplier.CreatedDate = DateTime.Now;
            supplier.UpdatedDate = DateTime.Now;
            supplier.Deleted = false;
            supplier.Status = SupplierStatus.Pending;
            await unitOfWork.SupplierRepository.AddAsync(supplier);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Tedarikçi kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);
        ValidateSupplier(supplier);

        var existing = await unitOfWork.SupplierRepository.FindAsync(id);
        if (existing is null) return false;

        // Email değiştiyse benzersizlik kontrolü
        if (!string.IsNullOrWhiteSpace(supplier.SupplierEmail) &&
            existing.SupplierEmail != supplier.SupplierEmail)
        {
            var emailExists = await GetSupplierByEmailAsync(supplier.SupplierEmail);
            if (emailExists is not null)
                throw new InvalidOperationException("Bu e-posta adresi başka bir tedarikçi tarafından kullanılıyor.");
        }

        try
        {
            mapper.Map(supplier, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Tedarikçi güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var supplier = await unitOfWork.SupplierRepository.FindAsync(id);
        if (supplier is null) return false;

        try
        {
            // Soft Delete
            supplier.Deleted = true;
            supplier.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierRepository.UpdateAsync(supplier);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Tedarikçi silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Supplier>> GetSuppliersByStatusAsync(SupplierStatus status)
    {
        return await unitOfWork.SupplierRepository.GetAllAsync(s =>
            s.Status == status && !s.Deleted);
    }

    public async Task<Supplier?> GetSupplierByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi gereklidir.", nameof(email));

        var suppliers = await unitOfWork.SupplierRepository.GetAllAsync(s =>
            s.SupplierEmail == email && !s.Deleted);

        return suppliers.FirstOrDefault();
    }

    public async Task<List<Supplier>> SearchSuppliersByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tedarikçi adı gereklidir.", nameof(name));

        return await unitOfWork.SupplierRepository.GetAllAsync(s =>
            s.SupplierName.Contains(name) && !s.Deleted);
    }

    public async Task<List<Supplier>> SearchSuppliersByShopNameAsync(string shopName)
    {
        if (string.IsNullOrWhiteSpace(shopName))
            throw new ArgumentException("Mağaza adı gereklidir.", nameof(shopName));

        return await unitOfWork.SupplierRepository.GetAllAsync(s =>
            s.ShopName.Contains(shopName) && !s.Deleted);
    }

    public async Task<List<Supplier>> GetSuppliersWithProductsAsync()
    {
        var suppliers = await unitOfWork.SupplierRepository.GetAllAsync(s => !s.Deleted);
        return suppliers.Where(s => s.Products.Count > 0).ToList();
    }

    public async Task<bool> UpdateStatusAsync(int supplierId, SupplierStatus status)
    {
        var supplier = await unitOfWork.SupplierRepository.FindAsync(supplierId);
        if (supplier is null) return false;

        try
        {
            supplier.Status = status;
            supplier.UpdatedDate = DateTime.Now;
            await unitOfWork.SupplierRepository.UpdateAsync(supplier);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Tedarikçi durumu güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> ActivateSupplierAsync(int supplierId)
    {
        return await UpdateStatusAsync(supplierId, SupplierStatus.Active);
    }

    public async Task<bool> DeactivateSupplierAsync(int supplierId)
    {
        return await UpdateStatusAsync(supplierId, SupplierStatus.Inactive);
    }

    public async Task<List<Supplier>> GetActiveSupplierAsync()
    {
        return await unitOfWork.SupplierRepository.GetAllAsync(s =>
            s.Status == SupplierStatus.Active && !s.Deleted);
    }

    public async Task<List<Supplier>> GetPendingSupplierAsync()
    {
        return await unitOfWork.SupplierRepository.GetAllAsync(s =>
            s.Status == SupplierStatus.Pending && !s.Deleted);
    }

    public async Task<double> GetAverageRatingAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        var reviews = await unitOfWork.ReviewRepository.GetAllAsync(r =>
            r.SupplierId == supplierId && !r.Deleted);

        if (reviews.Count == 0)
            return 0;

        return reviews.Average(r => r.Rating);
    }

    public async Task<int> GetTotalOrdersAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        var orders = await unitOfWork.OrderRepository.GetAllAsync(o =>
            o.SupplierId == supplierId && !o.Deleted);

        return orders.Count;
    }

    public async Task<List<Supplier>> GetSuppliersByFiltersAsync(
        string? name = null,
        string? shopName = null,
        string? email = null,
        string? phone = null,
        SupplierStatus? status = null)
    {
        return await unitOfWork.SupplierRepository.GetAllAsync(s =>
            !s.Deleted &&
            (string.IsNullOrEmpty(name) || s.SupplierName.Contains(name)) &&
            (string.IsNullOrEmpty(shopName) || s.ShopName.Contains(shopName)) &&
            (string.IsNullOrEmpty(email) || s.SupplierEmail == email) &&
            (string.IsNullOrEmpty(phone) || s.SupplierPhone.Contains(phone)) &&
            (!status.HasValue || s.Status == status.Value));
    }

    #endregion

    #region Validation

    private static void ValidateSupplier(Supplier supplier)
    {
        if (string.IsNullOrWhiteSpace(supplier.ShopName))
            throw new ArgumentException("Mağaza adı gereklidir.", nameof(supplier));

        if (supplier.ShopName.Length > 50)
            throw new ArgumentException("Mağaza adı 50 karakterden uzun olamaz.", nameof(supplier));

        if (string.IsNullOrWhiteSpace(supplier.SupplierName))
            throw new ArgumentException("Tedarikçi adı gereklidir.", nameof(supplier));

        if (supplier.SupplierName.Length > 50)
            throw new ArgumentException("Tedarikçi adı 50 karakterden uzun olamaz.", nameof(supplier));

        if (string.IsNullOrWhiteSpace(supplier.SupplierAddress))
            throw new ArgumentException("Tedarikçi adresi gereklidir.", nameof(supplier));

        if (supplier.SupplierAddress.Length > 500)
            throw new ArgumentException("Tedarikçi adresi 500 karakterden uzun olamaz.", nameof(supplier));

        if (string.IsNullOrWhiteSpace(supplier.SupplierPhone))
            throw new ArgumentException("Tedarikçi telefon numarası gereklidir.", nameof(supplier));

        if (!string.IsNullOrWhiteSpace(supplier.SupplierEmail) && !IsValidEmail(supplier.SupplierEmail))
            throw new ArgumentException("Geçersiz e-posta adresi.", nameof(supplier));
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}