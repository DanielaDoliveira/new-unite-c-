using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterAttendeeUseCase()
    {
        _dbContext = new PassInDbContext();

    }
    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
    {

        Validate(eventId, request);
        var entity = new Infrastructure.Entities.Attendee
        {
            Email = request.Email,
            Name = request.Name,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow,

        };
        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }
    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _dbContext.Events.Find(eventId);
        var attendeeAlreadyRegistered = _dbContext
        .Attendees
        .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);
        var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);

        if (eventEntity is null)
            throw new NotFoundException("Event not found!");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("The name is invalid");

        if (!EmailIsValid(request.Email))
            throw new ErrorOnValidationException("Email is invalid");

        if (attendeeAlreadyRegistered)
            throw new ConflictException("You cannot register twice on same event");

        if (attendeesForEvent >= eventEntity.Maximum_Attendees)
            throw new ErrorOnValidationException("There is no room for this event");

    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}