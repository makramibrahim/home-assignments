using Microsoft.AspNetCore.Mvc;
using PersonDemo.API.Models;
using PersonDemo.API.Services.interfaces;
using System.Net;

namespace PersonDemo.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PersonController : BaseAPIController
  {
    private readonly IPersonService _service;
    public PersonController(IPersonService service)
    {
      _service = service;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public ApiResult Get(Guid id)
    {
      DPerson result = _service.GetPerson(id);
      return ApiResult(HttpStatusCode.OK, "", result);
    }

    [HttpGet]
    [Route("all")]
    public ApiResult Get()
    {
      var result = _service.GetAll();
      return result.Count > 0
        ? ApiResult(HttpStatusCode.OK, "Success", result)
        : ApiResult(HttpStatusCode.NotFound, _service.ErrorMessage!);
    }

    [HttpPost]
    [Route("")]
    public ApiResult Create(DPerson person)
    {
      bool result = _service.CreatePerson(person);
      return result
        ? ApiResult(HttpStatusCode.OK, "Success", result)
        : ApiResult(HttpStatusCode.NotFound, _service.ErrorMessage!);
    }

    [HttpPut]
    [Route("")]
    public ApiResult Update(DPerson person)
    {
      bool result = _service.UpdatePerson(person);
      return result
        ? ApiResult(HttpStatusCode.OK, "Success", result)
        : ApiResult(HttpStatusCode.NotFound, _service.ErrorMessage!);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public ApiResult Delete(Guid id)
    {
      bool result = _service.Delete(id);
      return ApiResult(HttpStatusCode.OK, "", result);
    }
  }
}