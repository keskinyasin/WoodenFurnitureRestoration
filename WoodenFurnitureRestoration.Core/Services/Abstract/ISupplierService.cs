using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

public interface ISupplierService : IService<Supplier>
{
    Task<List<Supplier>> GetSuppliersByStatusAsync(SupplierStatus status);
    Task<Supplier?> GetSupplierByEmailAsync(string email);
    Task<List<Supplier>> SearchSuppliersByNameAsync(string name);
    Task<List<Supplier>> SearchSuppliersByShopNameAsync(string shopName);
    Task<List<Supplier>> GetSuppliersWithProductsAsync();
    Task<bool> UpdateStatusAsync(int supplierId, SupplierStatus status);
    Task<bool> ActivateSupplierAsync(int supplierId);
    Task<bool> DeactivateSupplierAsync(int supplierId);
    Task<List<Supplier>> GetActiveSupplierAsync();
    Task<List<Supplier>> GetPendingSupplierAsync();
    Task<double> GetAverageRatingAsync(int supplierId);
    Task<int> GetTotalOrdersAsync(int supplierId);
    Task<List<Supplier>> GetSuppliersByFiltersAsync(
        string? name = null,
        string? shopName = null,
        string? email = null,
        string? phone = null,
        SupplierStatus? status = null);
}