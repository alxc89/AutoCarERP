namespace AutoCarERP.Application.DTOs;

/// <summary>Resultado de paginação genérico.</summary>
public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount);