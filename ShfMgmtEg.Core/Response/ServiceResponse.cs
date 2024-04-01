using System.Net;

namespace ShfMgmtEg.Core.Response;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; } = false;
    public string? Message { get; set; } = string.Empty;

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Accepted;
}