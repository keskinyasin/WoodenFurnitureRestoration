using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    #region CRUD Operations

    public async Task<List<Product>> GetAllAsync()
    {
        return await unitOfWork.ProductRepository.GetAllAsync(p => !p.Deleted);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir ürün ID'si gereklidir.", nameof(id));

        return await unitOfWork.ProductRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        ValidateProduct(product);

        try
        {
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;
            product.Deleted = false;
            await unitOfWork.ProductRepository.AddAsync(product);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ürün kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        ValidateProduct(product);

        var existing = await unitOfWork.ProductRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(product, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.ProductRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ürün güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await unitOfWork.ProductRepository.FindAsync(id);
        if (product is null) return false;

        try
        {
            // Soft Delete
            product.Deleted = true;
            product.UpdatedDate = DateTime.Now;
            await unitOfWork.ProductRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Ürün silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(categoryId));

        return await unitOfWork.ProductRepository.GetAllAsync(p =>
            p.CategoryId == categoryId && !p.Deleted);
    }

    public async Task<List<Product>> GetProductsBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.ProductRepository.GetAllAsync(p =>
            p.SupplierId == supplierId && !p.Deleted);
    }

    public async Task<List<Product>> GetProductsBySupplierMaterialAsync(int supplierMaterialId)
    {
        if (supplierMaterialId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi malzeme ID'si gereklidir.", nameof(supplierMaterialId));

        return await unitOfWork.ProductRepository.GetAllAsync(p =>
            p.SupplierMaterialId == supplierMaterialId && !p.Deleted);
    }

    public async Task<List<Product>> SearchProductsByNameAsync(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Ürün adı gereklidir.", nameof(productName));

        return await unitOfWork.ProductRepository.GetAllAsync(p =>
            p.ProductName.Contains(productName) && !p.Deleted);
    }

    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        if (minPrice < 0)
            throw new ArgumentException("Minimum fiyat 0'dan küçük olamaz.", nameof(minPrice));

        if (maxPrice < minPrice)
            throw new ArgumentException("Maksimum fiyat minimum fiyattan küçük olamaz.", nameof(maxPrice));

        return await unitOfWork.ProductRepository.GetAllAsync(p =>
            p.Price >= minPrice && p.Price <= maxPrice && !p.Deleted);
    }

    public async Task<bool> UpdatePriceAsync(int productId, decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.", nameof(newPrice));

        var product = await unitOfWork.ProductRepository.FindAsync(productId);
        if (product is null) return false;

        try
        {
            product.Price = newPrice;
            product.UpdatedDate = DateTime.Now;
            await unitOfWork.ProductRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Fiyat güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<decimal> GetAveragePriceAsync()
    {
        var products = await unitOfWork.ProductRepository.GetAllAsync(p => !p.Deleted);

        if (products.Count == 0)
            return 0;

        return products.Average(p => p.Price);
    }

    public async Task<List<Product>> GetTopSellingProductsAsync(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Sayı 0'dan büyük olmalıdır.", nameof(count));

        // OrderDetails üzerinden en çok satılan ürünleri getir
        var products = await unitOfWork.ProductRepository.GetAllAsync(p => !p.Deleted);

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
        return await unitOfWork.ProductRepository.GetAllAsync(p =>
            !p.Deleted &&
            (!categoryId.HasValue || p.CategoryId == categoryId.Value) &&
            (!supplierId.HasValue || p.SupplierId == supplierId.Value) &&
            (!supplierMaterialId.HasValue || p.SupplierMaterialId == supplierMaterialId.Value) &&
            (string.IsNullOrEmpty(productName) || p.ProductName.Contains(productName)) &&
            (!minPrice.HasValue || p.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || p.Price <= maxPrice.Value));
    }

    #endregion

    #region Validation

    private static void ValidateProduct(Product product)
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

    #endregion
}