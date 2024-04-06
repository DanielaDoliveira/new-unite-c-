

using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Checkins;

public class DoAttendeeCheckinUseCase
{

    private readonly PassInDbContext _dbContext;
    public DoAttendeeCheckinUseCase()
    {
        _dbContext = new PassInDbContext();

    }
    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        Validate(attendeeId);
        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,

        };
        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();
        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }
    private void Validate(Guid attendeeId)
    {
        var existAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);
        var existCheckIn = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);
        if (!existAttendee)
        {
            throw new NotFoundException("The attendee with this ID was not found");
        }
        if (existCheckIn)
        {
            throw new ConflictException("Attendee cannot do checkIn twice in the same event");
        }

    }
}