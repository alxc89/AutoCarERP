namespace AutoCarERP.Application.Common.Interfaces;

public interface IAuditLogger
{
    Task LogAsync(string action, string entityName, string entityId, object? data = null, CancellationToken ct = default);
}
