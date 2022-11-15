using System.Net;

namespace DataLayer;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public HttpContent? Content { get; set; }
}