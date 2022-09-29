namespace SquareUp.Shared.Types;

public class ServiceResponse<T>
{
    public ServiceResponse(T? data = default, string message = "")
    {
        Data = data;
        Message = message;
    }

    public T? Data { get; }
    public bool Success => Data != null;
    public string Message { get; }
}