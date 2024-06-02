using System.Net;

namespace Rentify.Application.Models.Responses;

public class RequestResponse
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string Message { get; set; } = string.Empty;
}


public class RequestResponse<T> : RequestResponse
{
    public T Data { get; set; } = default!;
}