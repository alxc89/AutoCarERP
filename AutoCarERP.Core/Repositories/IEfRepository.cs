using System.Linq.Expressions;
using AutoCarERP.Core.Shared.Response;

namespace AutoCarERP.Core.Repositories;

public interface IEfRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct = default);
    Task Update(T entity);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<T?> GetByIdAsync(int id, bool includeDeleted = false, bool asNoTracking = false, CancellationToken ct = default);
    Task<PagedResult<T>> GetPagedAsync(
        Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        int page,
        int pageSize,
        bool includeDeleted = false,
        bool asNoTracking = true,
        CancellationToken ct = default);
}