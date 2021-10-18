using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPJ.EFCore.Initialize
{
    public interface IRepository<T>
    {
        T Create(T data);
        IEnumerable<T> Gets();
        T Get(object Id);
        T FindOne(Func<T, bool> condition);
        IEnumerable<T> Find(Func<T, bool> condition);
        T Update(object Id, T data);
        T Delete(object Id);
        void Save();
        Task<T> CreateAsync(T data);
        Task<IEnumerable<T>> GetsAsync();
        Task<T> GetAsync(object Id);
        Task<T> UpdateAsync(object Id, T data);
        Task<T> DeleteAsync(object Id);
        Task SaveAsync();
    }


}