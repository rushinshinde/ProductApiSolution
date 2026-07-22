namespace Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public T? Data { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(T data, string message)
    {
        Success = true;
        Message = message;
        Data = data;
    }
}