using Microsoft.EntityFrameworkCore;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Entities;

namespace WoodenFurnitureRestoration.Core.Services.Concrete;

/// <summary>
/// Tüm servisler için ortak CRUD implementasyonu
/// </summary>
public abstract class Service<T>(IUnitOfWork unitOfWork) : IService<T>
    where T : class, IEntity, new()
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;

    protected abstract IRepository<T> Repository { get; }

    protected virtual void ValidateEntity(T entity) { }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await Repository.GetAllAsync(e => !e.Deleted);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Geçerli bir ID gereklidir.", nameof(id));

        var entity = await Repository.FindAsync(id);
        return entity?.Deleted == true ? null : entity;
    }

    public virtual async Task<int> CreateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ValidateEntity(entity);

        entity.CreatedDate = DateTime.Now;
        entity.UpdatedDate = DateTime.Now;
        entity.Deleted = false;

        try
        {
            await Repository.AddAsync(entity);
            return await _unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kayıt eklenirken bir hata oluştu.", ex);
        }
    }

    public virtual async Task<bool> UpdateAsync(int id, T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ValidateEntity(entity);

        var existing = await Repository.FindAsync(id);
        if (existing is null || existing.Deleted) return false;

        entity.Id = id;
        entity.CreatedDate = existing.CreatedDate;
        entity.UpdatedDate = DateTime.Now;
        entity.Deleted = false;

        try
        {
            await Repository.UpdateAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Kayıt güncellenirken bir hata oluştu.", ex);
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await Repository.FindAsync(id);
        if (entity is null || entity.Deleted) return false;

        entity.Deleted = true;
        entity.UpdatedDate = DateTime.Now;

        await Repository.UpdateAsync(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        var entity = await Repository.FindAsync(id);
        return entity is not null && !entity.Deleted;
    }

    public virtual async Task<int> CountAsync()
    {
        var all = await Repository.GetAllAsync(e => !e.Deleted);
        return all.Count;
    }
}