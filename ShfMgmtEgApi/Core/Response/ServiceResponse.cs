namespace ShfMgmtEgApi.Core.Response;

public class ServiceResponse<T> where T : class
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}