using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PersonDemo.API.Controllers
{
  public class ApiResult
  {
    public HttpStatusCode Code { get; set; }

    public object? Data { get; set; }

    public string? Message { get; set; }
  }

  public class BaseAPIController : ControllerBase
  {
    protected ApiResult ApiResult(HttpStatusCode code, string message = "", object? data = null)
    {
      return new ApiResult
      {
        Code = code,
        Message = message,
        Data = data
      };
    }
  }
}
