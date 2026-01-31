using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WoodenFurnitureRestoration.Data.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        // ✅ SYNCHRONOUS METHODS
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Find(int id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        int Save();

        // ✅ ASYNCHRONOUS METHODS
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> FindAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveAsync();


    }
}
