using System.Linq.Expressions;
using Euroins.Payment.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ZvadoHacks.Data.Repositories;

public abstract class GenericRepository<T> : IRepository<T>
    where T : class
{
    protected AppDbContext _dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T> Add(T entity)
    {
        var result = await _dbContext.AddAsync(entity);
        await SaveChanges();
        return result.Entity;
    }

    public virtual async Task<T> Delete(T entity)
    {
        _dbContext.Remove(entity);

        await SaveChanges();

        return entity;
    }

    public virtual Task<bool> Exists(Expression<Func<T, bool>> predicate)
        => _dbContext
            .Set<T>()
            .AnyAsync(predicate);

    public async Task<GetPaginatedDto<T>> GetPaginated(int pageNumber, int pageSize,
        Expression<Func<T, bool>>? filterExpression = null,
        Expression<Func<T, object>>? sortExpression = null,
        bool isAscending = false)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var skip = (pageNumber - 1) * pageSize;
        var query = _dbContext.Set<T>().AsQueryable();

        if (filterExpression != null)
        {
            query = query.Where(filterExpression);
        }

        if (sortExpression != null)
        {
            query = isAscending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);
        }

        var total = await query.CountAsync();

        var result = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new GetPaginatedDto<T>()
        {
            Items = result,
            TotalCount = total
        };
    }

    public virtual async Task<IEnumerable<T>> FindMany(Expression<Func<T, bool>> predicate)
        => await _dbContext
            .Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToListAsync();

    public virtual async Task<T?> FindOne(Expression<Func<T, bool>> predicate)
        => await _dbContext
            .Set<T>()
            .AsQueryable()
            .FirstOrDefaultAsync(predicate);

    public virtual async Task<List<T>> GetAll()
        => await _dbContext
            .Set<T>()
            .ToListAsync();

    public virtual async Task<T> Update(T entity)
    {
        _dbContext.Update(entity);

        await SaveChanges();

        return entity;
    }

    public async Task<bool> SaveChanges()
        => await _dbContext.SaveChangesAsync() < 0;

    public async Task Delete(IEnumerable<T> values)
    {
        _dbContext.Set<T>().RemoveRange(values);

        await SaveChanges();
    }
}