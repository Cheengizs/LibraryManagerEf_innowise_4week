using Application.Commons;

public class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public List<string>? Errors { get; init; }
    public ServiceErrorCode ErrorCode { get; init; } = ServiceErrorCode.None;

    private ServiceResult(bool isSuccess, T? value = default, List<string>? errors = null, ServiceErrorCode errorCode = ServiceErrorCode.None)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        ErrorCode = errorCode;
    }

    public static ServiceResult<T> Success(T value) => new(true, value);

    public static ServiceResult<T> Failure(List<string> errors, ServiceErrorCode errorCode)
        => new(false, default, errors, errorCode);
}

public class ServiceResult
{
    public bool IsSuccess { get; init; }
    public List<string>? Errors { get; init; }
    public ServiceErrorCode ErrorCode { get; init; } = ServiceErrorCode.None;

    private ServiceResult(bool isSuccess, List<string>? errors = null, ServiceErrorCode errorCode = ServiceErrorCode.None)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        ErrorCode = errorCode;
    }

    public static ServiceResult Success() => new(true);

    public static ServiceResult Failure(List<string> errors, ServiceErrorCode errorCode)
        => new(false, errors, errorCode);
}