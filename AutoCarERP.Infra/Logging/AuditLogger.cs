using System.Text.Json;
using AutoCarERP.Application.Common.Interfaces;
using AutoCarERP.Core.Entities;
using AutoCarERP.Infra.EF;

namespace AutoCarERP.Infra.Logging;

public class AuditLogger : IAuditLogger
{
    private readonly AppDbContext _context;
    private readonly IUserContext _userContext;

    public AuditLogger(AppDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task LogAsync(string action, string entityName, string entityId, object? data = null, CancellationToken ct = default)
    {
        var entry = new AuditLog
        {
            UserId = _userContext.UserId ?? string.Empty,
            UserName = _userContext.UserName ?? string.Empty,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            Data = data is null ? null : JsonSerializer.Serialize(data),
            CreatedAt = DateTime.UtcNow
        };

        await _context.AuditLogs.AddAsync(entry, ct);
        await _context.SaveChangesAsync(ct);
    }
}
