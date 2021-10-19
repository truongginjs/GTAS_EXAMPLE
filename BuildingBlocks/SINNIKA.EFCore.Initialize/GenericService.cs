using System.Collections.Generic;
using System.Threading.Tasks;

namespace SINNIKA.EFCore.Initialize
{
    public abstract class GenericService<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        protected GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual T Create(T data) => _repository.Create(data);

        public virtual Task<T> CreateAsync(T data) => _repository.CreateAsync(data);

        public virtual T Delete(object Id) => _repository.Delete(Id);

        public virtual Task<T> DeleteAsync(object Id) => _repository.DeleteAsync(Id);

        public virtual T Get(object Id) => _repository.Get(Id);

        public virtual Task<T> GetAsync(object Id) => _repository.GetAsync(Id);

        public virtual IEnumerable<T> Gets() => _repository.Gets();

        public virtual Task<IEnumerable<T>> GetsAsync() => _repository.GetsAsync();

        public TResult Invoke<TResult>(System.Func<IRepository<T>, TResult> func) => func(_repository);

        public Task<TResult> InvokeAsync<TResult>(System.Func<IRepository<T>, Task<TResult>> func) => func(_repository);

        public virtual T Update(object Id, T data) => _repository.Update(Id, data);

        public virtual Task<T> UpdateAsync(object Id, T data) => _repository.UpdateAsync(Id, data);
    }
}