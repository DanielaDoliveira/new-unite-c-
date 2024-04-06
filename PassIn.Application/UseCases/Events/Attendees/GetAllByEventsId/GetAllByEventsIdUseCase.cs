

using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Attendees.GetAllByEventsId;

public class GetAllByEventsIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetAllByEventsIdUseCase()
    {
        _dbContext = new PassInDbContext();

    }
    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefault(ev => ev.Id == eventId);
        if (entity is null)
            throw new NotFoundException("Event does not exist ");
        return new ResponseAllAttendeesJson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at,
            }).ToList()
        };
    }
}