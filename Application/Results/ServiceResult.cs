namespace Application.Common;

public class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public List<string>? Errors { get; init; }

    private ServiceResult(bool isSuccess, T? value = default, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
    }

    public static ServiceResult<T> Success(T value) => new(true, value);

    public static ServiceResult<T> Failure(List<string> errors) => new(false, default, errors);
}

public class ServiceResult
{
    public bool IsSuccess { get; init; }
    public List<string>? Errors { get; init; }

    private ServiceResult(bool isSuccess, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static ServiceResult Success() => new(true);

    public static ServiceResult Failure(List<string> errors) => new(false, errors);
}