namespace AutoCarERP.Application.Common.Interfaces;

public interface IUserContext
{
    string? UserId { get; }
    string? UserName { get; }
    IReadOnlyCollection<string> Roles { get; }
    IReadOnlyCollection<string> Permissions { get; }
}
