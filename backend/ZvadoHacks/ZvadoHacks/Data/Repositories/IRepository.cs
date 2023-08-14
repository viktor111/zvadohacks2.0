using System.Linq.Expressions;
using ZvadoHacks.Data.Repositories;

namespace Euroins.Payment.Data.Repositories;

public interface IRepository<T> where T : class
{
    public Task<T> Add(T entity);

    public Task<T> Update(T entity);

    public Task<T?> FindOne(Expression<Func<T, bool>> predicate);

    public Task<IEnumerable<T>> FindMany(Expression<Func<T, bool>> predicate);

    public Task<T> Delete(T entity);

    public Task Delete(IEnumerable<T> values);

    public Task<List<T>> GetAll();

    public Task<bool> Exists(Expression<Func<T, bool>> predicate);

    public Task<GetPaginatedDto<T>> GetPaginated(int pageNumber, int pageSize,
          Expression<Func<T, bool>>? filterExpression = null,
          Expression<Func<T, object>>? sortExpression = null,
          bool isAscending = false);

    public Task<bool> SaveChanges();
}