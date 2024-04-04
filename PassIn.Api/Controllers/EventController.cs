
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;


namespace PassIn.Api.Controllers;

[Route("[controller]")]
[ApiController]

public class EventController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {


        var useCase = new RegisterUseCase();
        var response = useCase.Execute(request);
        return Created(string.Empty, response);



    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetEventByIdUseCase();
        var response = useCase.Execute(id);
        return Ok(response);



    }


}