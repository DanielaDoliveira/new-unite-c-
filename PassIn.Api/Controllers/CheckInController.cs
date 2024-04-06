
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Checkins;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckInController : ControllerBase
{
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult CheckIn([FromRoute] Guid attendeeId)
    {
        var useCase = new DoAttendeeCheckinUseCase();
        var response = useCase.Execute(attendeeId);
        return Created(string.Empty, response);
    }
}