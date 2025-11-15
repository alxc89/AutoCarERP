namespace AutoCarERP.Core.Shared.Response;

public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount);