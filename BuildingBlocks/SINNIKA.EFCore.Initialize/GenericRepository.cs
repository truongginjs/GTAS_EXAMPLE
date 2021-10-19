using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINNIKA.EFCore.Initialize
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _table;
        public GenericRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual T Create(T data)
        {
            _table.Add(data);
            Save();
            return data;
        }

        public virtual async Task<T> CreateAsync(T data)
        {
            await _table.AddAsync(data);
            await SaveAsync();
            return data;
        }

        public virtual T Delete(object Id)
        {
            T t = _table.Find(Id);
            _table.Remove(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<T> DeleteAsync(object Id)
        {
            T t = await _table.FindAsync(Id);
            _table.Remove(t);
            await SaveAsync();
            return t;
        }


        public virtual T Get(object Id) => _table.Find(Id);

        public virtual async Task<T> GetAsync(object Id) => await _table.FindAsync(Id);

        public virtual IEnumerable<T> Gets() => _table.ToList();

        public virtual async Task<IEnumerable<T>> GetsAsync() => await _table.ToListAsync();

        public TResult Invoke<TResult>(Func<DbContext, TResult> func)=>func(_context);
        public TResult Invoke<TResult>(Func<DbSet<T>, TResult> func)=>func(_table);

        public Task<TResult> InvokeAsync<TResult>(Func<DbContext, Task<TResult>> func)=>func(_context);

        public Task<TResult> InvokeAsync<TResult>(Func<DbSet<T>, Task<TResult>> func)=>func(_table);

        public virtual void Save() => _context.SaveChanges();


        public virtual async Task SaveAsync() => await _context.SaveChangesAsync();

        public virtual T Update(object Id, T data)
        {
            var entity = _table.Find(Id);
            if (entity == null) return default(T);
            _context.Entry(entity).CurrentValues.SetValues(data);
            Save();
            return data;
        }

        public virtual async Task<T> UpdateAsync(object Id, T data)
        {
            var entity = _table.Find(Id);
            if (entity == null) return default(T);
            _context.Entry(entity).CurrentValues.SetValues(data);
            await SaveAsync();
            return data;
        }
    }
}
