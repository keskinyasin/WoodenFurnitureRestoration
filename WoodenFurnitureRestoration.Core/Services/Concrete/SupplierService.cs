using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Supplier>(unitOfWork), ISupplierService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Supplier> Repository => unitOfWork.SupplierRepository;

    protected override void ValidateEntity(Supplier supplier)
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

    public async Task<List<Supplier>> GetSuppliersByStatusAsync(SupplierStatus status)
    {
        return await Repository.GetAllAsync(s => s.Status == status && !s.Deleted);
    }

    public async Task<Supplier?> GetSupplierByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("E-posta adresi gereklidir.", nameof(email));
        var suppliers = await Repository.GetAllAsync(s => s.SupplierEmail == email && !s.Deleted);
        return suppliers.FirstOrDefault();
    }

    public async Task<List<Supplier>> SearchSuppliersByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tedarikçi adı gereklidir.", nameof(name));
        return await Repository.GetAllAsync(s => s.SupplierName.Contains(name) && !s.Deleted);
    }

    public async Task<List<Supplier>> SearchSuppliersByShopNameAsync(string shopName)
    {
        if (string.IsNullOrWhiteSpace(shopName))
            throw new ArgumentException("Mağaza adı gereklidir.", nameof(shopName));
        return await Repository.GetAllAsync(s => s.ShopName.Contains(shopName) && !s.Deleted);
    }

    public async Task<List<Supplier>> GetSuppliersWithProductsAsync()
    {
        var suppliers = await Repository.GetAllAsync(s => !s.Deleted);
        return suppliers.Where(s => s.Products.Count > 0).ToList();
    }

    public async Task<bool> UpdateStatusAsync(int supplierId, SupplierStatus status)
    {
        var supplier = await Repository.FindAsync(supplierId);
        if (supplier is null) return false;

        supplier.Status = status;
        supplier.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(supplier);
        await unitOfWork.SaveChangesAsync();
        return true;
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
        return await Repository.GetAllAsync(s => s.Status == SupplierStatus.Active && !s.Deleted);
    }

    public async Task<List<Supplier>> GetPendingSupplierAsync()
    {
        return await Repository.GetAllAsync(s => s.Status == SupplierStatus.Pending && !s.Deleted);
    }

    public async Task<double> GetAverageRatingAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));
        var reviews = await unitOfWork.ReviewRepository.GetAllAsync(r => r.SupplierId == supplierId && !r.Deleted);
        if (reviews.Count == 0)
            return 0;
        return reviews.Average(r => r.Rating);
    }

    public async Task<int> GetTotalOrdersAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));
        var orders = await unitOfWork.OrderRepository.GetAllAsync(o => o.SupplierId == supplierId && !o.Deleted);
        return orders.Count;
    }

    public async Task<List<Supplier>> GetSuppliersByFiltersAsync(
        string? name = null,
        string? shopName = null,
        string? email = null,
        string? phone = null,
        SupplierStatus? status = null)
    {
        return await Repository.GetAllAsync(s =>
            !s.Deleted &&
            (string.IsNullOrEmpty(name) || s.SupplierName.Contains(name)) &&
            (string.IsNullOrEmpty(shopName) || s.ShopName.Contains(shopName)) &&
            (string.IsNullOrEmpty(email) || s.SupplierEmail == email) &&
            (string.IsNullOrEmpty(phone) || s.SupplierPhone.Contains(phone)) &&
            (!status.HasValue || s.Status == status.Value));
    }
}