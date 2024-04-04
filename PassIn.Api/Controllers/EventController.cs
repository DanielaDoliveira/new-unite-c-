
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

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

        try
        {
            var useCase = new RegisterUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
        catch (PassInException error)
        {
            return BadRequest(new ResponseErrorJson(error.Message));
        }

        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson(" Unknown Error. "));
        }
    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        try
        {
            var useCase = new GetEventByIdUseCase();
            var response = useCase.Execute(id);
            return Ok(response);
        }
        catch (PassInException error)
        {
            return NotFound(new ResponseErrorJson(error.Message));
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson(" Unknown Error. "));
        }

    }


}