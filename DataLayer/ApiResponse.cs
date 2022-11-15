using System.Net;

namespace DataLayer;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Content { get; set; }
}