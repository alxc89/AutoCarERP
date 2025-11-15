using System.Linq.Expressions;
using AutoCarERP.Core.Entities;
using AutoCarERP.Core.Repositories;
using AutoCarERP.Core.Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace AutoCarERP.Infra.EF.Repositories;

public class EfRepository<T> : IEfRepository<T> where T : Entity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfRepository(AppDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _dbSet.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(id, false, false, ct);
        if (entity is null) return;

        _dbSet.Remove(entity);

        await _context.SaveChangesAsync(ct);
    }

    public async Task<T?> GetByIdAsync(int id, bool includeDeleted = false, bool asNoTracking = false,
        CancellationToken ct = default)
    {
        IQueryable<T> query = _dbSet.Where(x => x.Codigo.Equals(id));

        if (asNoTracking)
            query = _dbSet.AsNoTracking();

        if (!includeDeleted)
            query = ApplyNotDeletedFilter(query);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<PagedResult<T>> GetPagedAsync(
        Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        int page,
        int pageSize,
        bool includeDeleted = false,
        bool asNoTracking = true,
        CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 20;

        IQueryable<T> query = _dbSet;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (!includeDeleted)
            query = ApplyNotDeletedFilter(query);

        if (filter is not null)
            query = query.Where(filter);

        var total = await query.CountAsync(ct);

        if (orderBy is not null)
            query = orderBy(query);
        else
            query = ApplyDefaultOrdering(query); // tenta ordenar por "Id" se existir, para paginação estável

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<T>(items, page, pageSize, total);
    }

    private static IQueryable<T> ApplyNotDeletedFilter(IQueryable<T> query)
    {
        var isDeletedProp = typeof(T).GetProperty("IsDeleted");
        if (isDeletedProp is null || isDeletedProp.PropertyType != typeof(bool))
            return query; // entidade não tem soft-delete

        // e => e.IsDeleted == false
        var p = Expression.Parameter(typeof(T), "e");
        var prop = Expression.Property(p, isDeletedProp);
        var cond = Expression.Equal(prop, Expression.Constant(false));
        var lambda = Expression.Lambda<Func<T, bool>>(cond, p);
        return query.Where(lambda);
    }

    private static IQueryable<T> ApplyDefaultOrdering(IQueryable<T> query)
    {
        var idProp = typeof(T).GetProperty("Id");
        if (idProp is not null && idProp.PropertyType == typeof(Guid))
        {
            var p = Expression.Parameter(typeof(T), "e");
            var prop = Expression.Property(p, idProp);
            var lambda = Expression.Lambda<Func<T, Guid>>(prop, p);
            return query.OrderBy(lambda);
        }

        return query;
    }

    private static bool IsSoftDeleted(object entity)
    {
        var isDeletedProp = entity.GetType().GetProperty("IsDeleted");
        if (isDeletedProp is null || isDeletedProp.PropertyType != typeof(bool))
            return false;

        return (bool)(isDeletedProp.GetValue(entity) ?? false);
    }
}