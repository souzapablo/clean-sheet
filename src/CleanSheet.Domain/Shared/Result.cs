using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Shared;

public class Result<T>
{
    private Result(Error error)
    {
        Error = error;
    }
    
    private Result(T data)
    {
        Data = data;
        IsSuccess = true;
    }
    
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public Error? Error { get; private set; }

    public static Result<T> Success(T data) => new(data);
    public static Result<T> Failure(Error error) => new(error);
}

