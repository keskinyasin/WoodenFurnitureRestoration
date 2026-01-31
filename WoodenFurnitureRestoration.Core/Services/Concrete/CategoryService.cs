using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    #region CRUD Operations

    public async Task<List<Category>> GetAllAsync()
    {
        return await unitOfWork.CategoryRepository.GetAllAsync(c => !c.Deleted);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir kategori ID'si gereklidir.", nameof(id));

        return await unitOfWork.CategoryRepository.FindAsync(id);
    }

    public async Task<int> CreateAsync(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        ValidateCategory(category);

        try
        {
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            await unitOfWork.CategoryRepository.AddAsync(category);
            return await unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kategori kaydedilirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> UpdateAsync(int id, Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        ValidateCategory(category);

        var existing = await unitOfWork.CategoryRepository.FindAsync(id);
        if (existing is null) return false;

        try
        {
            mapper.Map(category, existing);
            existing.UpdatedDate = DateTime.Now;
            await unitOfWork.CategoryRepository.UpdateAsync(existing);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kategori güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await unitOfWork.CategoryRepository.FindAsync(id);
        if (category is null) return false;

        try
        {
            // Soft delete
            category.Deleted = true;
            category.UpdatedDate = DateTime.Now;
            await unitOfWork.CategoryRepository.UpdateAsync(category);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kategori silinirken bir hata oluştu.", ex);
        }
    }

    #endregion

    #region Custom Business Methods

    public async Task<List<Category>> GetCategoriesByFiltersAsync(
        bool? isActive = null,
        string? name = null,
        string? description = null)
    {
        return await unitOfWork.CategoryRepository.GetAllAsync(c =>
            !c.Deleted &&
            (!isActive.HasValue || c.Deleted != isActive.Value) &&
            (string.IsNullOrEmpty(name) || c.CategoryName.Contains(name)) &&
            (string.IsNullOrEmpty(description) || c.CategoryDescription.Contains(description)));
    }

    public async Task<List<Category>> GetCategoriesBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await unitOfWork.CategoryRepository.GetAllAsync(c =>
            !c.Deleted && c.SupplierId == supplierId);
    }

    public async Task<List<Category>> GetCategoriesByCityAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Şehir adı gereklidir.", nameof(city));

        return await unitOfWork.CategoryRepository.GetCategoryNameByCustomerAndAddressAsync(city);
    }

    #endregion

    #region Validation

    private static void ValidateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.CategoryName))
            throw new ArgumentException("Kategori adı gereklidir.", nameof(category));

        if (string.IsNullOrWhiteSpace(category.CategoryDescription))
            throw new ArgumentException("Kategori açıklaması gereklidir.", nameof(category));

        if (category.CategoryName.Length > 100)
            throw new ArgumentException("Kategori adı 100 karakterden uzun olamaz.", nameof(category));

        if (category.CategoryDescription.Length > 500)
            throw new ArgumentException("Kategori açıklaması 500 karakterden uzun olamaz.", nameof(category));

        if (category.SupplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi seçilmelidir.", nameof(category));
    }

    #endregion
}