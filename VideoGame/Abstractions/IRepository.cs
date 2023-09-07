using System.Linq.Expressions;

namespace VideoGame.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
       // Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync<TProperty>(Expression<Func<T, TProperty>> include, Expression<Func<T, bool>>? where = null);
        Task<T?> GetAsync(int id);
        Task<T?> GetAllRelatedDataAsync<TProperty>(int id, Expression<Func<T, TProperty>> include);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
