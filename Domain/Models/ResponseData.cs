namespace Domain.Models;

public class ResponseData<T>
{
    public T? Data { get; set; }
    public bool Successful { get; set; }
    public string? ErrorMessage { get; set; }

    public static ResponseData<T> Success(T data)
    {
        return new ResponseData<T> { Data = data, Successful = true };
    }

    public static ResponseData<T> Fail(string message, T? data = default)
    {
        return new ResponseData<T> { Data = data, Successful = false, ErrorMessage = message };
    }
}