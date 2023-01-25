using App.DataAccess.Contexts;
using App.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly AppDbContex _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContex context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
         return _table.AsQueryable();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
         return _table.Where(predicate);
        }

        public T GetById(int id)
        {
            return _table.Find(id);
        }

        public void Create(T entity)
        {
          _table.Add(entity);
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

     
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
