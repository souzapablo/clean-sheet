namespace CleanSheet.Domain.Shared;

public class TypedResult<T> : Result
{
    private TypedResult(Error error) 
        :base(error) { }
    
    private TypedResult(T data)
    {
        Data = data;
    }
    
    public T? Data { get; private set; }

    public new static TypedResult<T> Success(T data) => new(data);
    public new static TypedResult<T> Failure(Error error) => new(error);
}

