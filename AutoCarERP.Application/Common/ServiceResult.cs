namespace AutoCarERP.Application.Common;

public record ServiceResult(bool Success, string? Error = null)
{
    public static ServiceResult Ok() => new(true);
    public static ServiceResult Fail(string error) => new(false, error);
}

public record ServiceResult<T>(bool Success, T? Value = default, string? Error = null)
{
    public static ServiceResult<T> Ok(T value) => new(true, value);
    public static ServiceResult<T> Fail(string error) => new(false, default, error);
}

