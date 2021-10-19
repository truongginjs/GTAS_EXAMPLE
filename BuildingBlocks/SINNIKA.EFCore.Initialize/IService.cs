using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SINNIKA.EFCore.Initialize
{
    public interface IService<T> where T : class
    {
        T Create(T data);
        Task<T> CreateAsync(T data);
        IEnumerable<T> Gets();
        Task<IEnumerable<T>> GetsAsync();
        T Get(object Id);
        Task<T> GetAsync(object Id);
        T Update(object Id, T data);
        Task<T> UpdateAsync(object Id, T data);
        T Delete(object Id);
        Task<T> DeleteAsync(object Id);

        TResult Invoke<TResult>(Func<IRepository<T>, TResult> func);
        Task<TResult> InvokeAsync<TResult>(Func<IRepository<T>, Task<TResult>> func);
    }
}