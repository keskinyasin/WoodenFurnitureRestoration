using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    : Service<Category>(unitOfWork), ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    protected override IRepository<Category> Repository => _unitOfWork.CategoryRepository;

    #region Custom Business Methods

    public async Task<List<Category>> GetCategoriesByFiltersAsync(
        bool? isActive = null,
        string? name = null,
        string? description = null)
    {
        return await Repository.GetAllAsync(c =>
            !c.Deleted &&
            (!isActive.HasValue || c.Deleted != isActive.Value) &&
            (string.IsNullOrEmpty(name) || c.CategoryName.Contains(name)) &&
            (string.IsNullOrEmpty(description) || c.CategoryDescription.Contains(description)));
    }

    public async Task<List<Category>> GetCategoriesBySupplierAsync(int supplierId)
    {
        if (supplierId <= 0)
            throw new ArgumentException("Geçerli bir tedarikçi ID'si gereklidir.", nameof(supplierId));

        return await Repository.GetAllAsync(c =>
            !c.Deleted && c.SupplierId == supplierId);
    }

    public async Task<List<Category>> GetCategoriesByCityAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Şehir adı gereklidir.", nameof(city));

        // Eğer özel bir repository metodu varsa kullan, yoksa uygun şekilde düzenle
        return await _unitOfWork.CategoryRepository.GetCategoryNameByCustomerAndAddressAsync(city);
    }

    #endregion

    #region Validation

    private static void ValidateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.CategoryName))
            throw new ArgumentException("Kategori adı gereklidir.", nameof(category));

        if (category.CategoryName.Length > 100)
            throw new ArgumentException("Kategori adı 100 karakterden uzun olamaz.", nameof(category));

        if (!string.IsNullOrWhiteSpace(category.CategoryDescription) && category.CategoryDescription.Length > 500)
            throw new ArgumentException("Kategori açıklaması 500 karakterden uzun olamaz.", nameof(category));
    }

    #endregion
}