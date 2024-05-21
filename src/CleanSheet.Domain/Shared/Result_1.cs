namespace CleanSheet.Domain.Shared;

public class Result<T> : Result
{
    protected Result(Error error) 
        :base(error) { }
    
    private Result(T data)
    {
        Data = data;
    }
    
    public T? Data { get; private set; }

    public new static Result<T> Success(T data) => new(data);
    public new static Result<T> Failure(Error error) => new(error);
}

