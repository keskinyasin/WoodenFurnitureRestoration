using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Abstract;

/// <summary>
/// Tüm servisler için ortak CRUD operasyonlarını tanımlar
/// </summary>
/// <typeparam name="T">Entity tipi (IEntity implement etmeli)</typeparam>
public interface IService<T> where T : class, IEntity
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task<bool> UpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> CountAsync();
}