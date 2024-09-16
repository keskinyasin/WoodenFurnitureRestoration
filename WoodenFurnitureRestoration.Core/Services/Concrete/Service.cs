using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoodenFurnitureRestoration.Core.Services.Abstract;
using WoodenFurnitureRestoration.Data.DbContextt;
using WoodenFurnitureRestoration.Data.Repositories.Abstract;
using WoodenFurnitureRestoration.Data.Repositories.Concrete;
using WoodenFurnitureRestoration.Entities;
using static System.Collections.Specialized.BitVector32;

namespace WoodenFurnitureRestoration.Core.Services.Concrete
{
    public class Service<T> : IService<T> where T : class, IEntity, new()
    {

        private readonly Repository<T> _repository;
        private readonly WoodenFurnitureRestorationContext _context;

        public Service(Repository<T> repository, WoodenFurnitureRestorationContext context)
        {
            _repository = repository;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
            _repository.Save();
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveAsync(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _repository.Save();
        }

        public T Find(int id)
        {
            return _repository.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _repository.GetAll(expression);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.GetAllAsync(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.GetAsync(expression);
        }

        public int Save()
        {
            return _repository.Save();
        }

        public async Task<int> SaveAsync(T entity)
        {
            return await _repository.SaveAsync(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _repository.Save();
        }
    }
}
