using AutoMapper;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Product>(unitOfWork), IProductService
{
    private readonly IMapper _mapper = mapper;
    protected override IRepository<Product> Repository => unitOfWork.ProductRepository;

    protected override void ValidateEntity(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.ProductName))
            throw new ArgumentException("Ürün adı gereklidir.", nameof(product));
        if (product.ProductName.Length > 100)
            throw new ArgumentException("Ürün adı 100 karakterden uzun olamaz.", nameof(product));
        if (string.IsNullOrWhiteSpace(product.Description))
            throw new ArgumentException("Ürün açıklaması gereklidir.", nameof(product));
        if (product.Description.Length > 500)
            throw new ArgumentException("Ürün açıklaması 500 karakterden uzun olamaz.", nameof(product));
        if (product.Price <= 0)
            throw new ArgumentException("Ürün fiyatı 0'dan büyük olmalıdır.", nameof(product));
        if (product.CategoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori seçilmelidir.", nameof(product));
        if (product.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(product));
        if (product.SupplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme seçilmelidir.", nameof(product));
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));
        return await Repository.GetAllAsync(p => p.CategoryId == categoryId && !p.Deleted);
    }

    public async Task<List<Product>> GetProductsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));
        return await Repository.GetAllAsync(p => p.SupplierId == supplierId && !p.Deleted);
    }

    public async Task<List<Product>> GetProductsBySupplierMaterialAsync(int supplierMaterialId)
    {
        if (supplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme ID'si gereklidir.", nameof(supplierMaterialId));
        return await Repository.GetAllAsync(p => p.SupplierMaterialId == supplierMaterialId && !p.Deleted);
    }

    public async Task<List<Product>> SearchProductsByNameAsync(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Ürün adı gereklidir.", nameof(productName));
        return await Repository.GetAllAsync(p => p.ProductName.Contains(productName) && !p.Deleted);
    }

    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));
        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));
        return await Repository.GetAllAsync(p => p.Price >= minPrice && p.Price <= maxPrice && !p.Deleted);
    }

    public async Task<bool> UpdatePriceAsync(int productId, decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(newPrice));
        var product = await Repository.FindAsync(productId);
        if (product is null) return false;

        product.Price = newPrice;
        product.UpdatedDate = DateTime.Now;
        await Repository.UpdateAsync(product);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        var products = await Repository.GetAllAsync(p => !p.Deleted);
        if (products.Count == 0)
            return 0;
        return products.Average(p => p.Price);
    }

    public async Task<List<Product>> GetTopSellingProductsAsync(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Sayı 0'dan büyük olmalıdır.", nameof(count));
        var products = await Repository.GetAllAsync(p => !p.Deleted);
        return products
            .OrderByDescending(p => p.OrderDetails.Count)
            .Take(count)
            .ToList();
    }

    public async Task<List<Product>> GetProductsByFiltersAsync(
        int? categoryId = null,
        int? supplierId = null,
        int? supplierMaterialId = null,
        string? productName = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        return await Repository.GetAllAsync(p =>
            !p.Deleted &&
            (!categoryId.HasValue || p.CategoryId == categoryId.Value) &&
            (!supplierId.HasValue || p.SupplierId == supplierId.Value) &&
            (!supplierMaterialId.HasValue || p.SupplierMaterialId == supplierMaterialId.Value) &&
            (string.IsNullOrEmpty(productName) || p.ProductName.Contains(productName)) &&
            (!minPrice.HasValue || p.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || p.Price <= maxPrice.Value));
    }
}