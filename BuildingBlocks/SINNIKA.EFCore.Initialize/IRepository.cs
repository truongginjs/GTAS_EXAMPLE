using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SINNIKA.EFCore.Initialize
{
    public interface IRepository<T> where T : class
    {
        T Create(T data);
        Task<T> CreateAsync(T data);
        IEnumerable<T> Gets();
        T Get(object Id);
        Task<T> GetAsync(object Id);
        Task<IEnumerable<T>> GetsAsync();
        T Update(object Id, T data);
        Task<T> UpdateAsync(object Id, T data);
        T Delete(object Id);
        Task<T> DeleteAsync(object Id);
        void Save();
        Task SaveAsync();
        TResult Invoke<TResult>(Func<DbContext, TResult> func);
        Task<TResult> InvokeAsync<TResult>(Func<DbContext, Task<TResult>> func);

        TResult Invoke<TResult>(Func<DbSet<T>, TResult> func);
        Task<TResult> InvokeAsync<TResult>(Func<DbSet<T>, Task<TResult>> func);
    }


}