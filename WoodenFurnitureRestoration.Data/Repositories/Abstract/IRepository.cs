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
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Find(int id);
        T Get(int id);
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        int Save();

        Task AddAsync(T entity);
        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveAsync(T entity);


    }
}
