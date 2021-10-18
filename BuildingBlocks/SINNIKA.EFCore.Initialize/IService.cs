using System.Collections.Generic;
using System.Threading.Tasks;

namespace SINNIKA.EFCore.Initialize
{
    public interface IService<T>
    {
        T Create(T data);
        IEnumerable<T> Gets();
        T Get(object Id);
        T Update(object Id, T data);
        T Delete(object Id);

        Task<T> CreateAsync(T data);
        Task<IEnumerable<T>> GetsAsync();
        Task<T> GetAsync(object Id);
        Task<T> UpdateAsync(object Id, T data);
        Task<T> DeleteAsync(object Id);
    }
}