
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Attendees.GetAllByEventsId;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
namespace PassIn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendeesController : ControllerBase
{
    [HttpPost]
    [Route("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult Register([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        var useCase = new RegisterAttendeeUseCase();
        var response = useCase.Execute(eventId, request);
        return Created(string.Empty, response);
    }
    [HttpGet]
    [Route("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public IActionResult GetAll([FromRoute] Guid eventId)
    {
        var useCase = new GetAllByEventsIdUseCase();
        var response = useCase.Execute(eventId);
        return Ok(response);
    }

}