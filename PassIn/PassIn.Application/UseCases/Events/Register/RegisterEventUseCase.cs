
using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Security.Cryptography.X509Certificates;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var entity = new Infrastructure.Entities.Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-")

        };
        dbContext.Events.Add(entity);
        dbContext.SaveChanges();
    }

    private void Validate(RequestEventJson resquest)
    {
        if(resquest.MaximumAttendees < 0)
        {
            throw new PassInException("The maximum attendees is invalid");
        }

        if (string.IsNullOrWhiteSpace(resquest.Title))
        {
            throw new PassInException("The title is invalid");
        }

        if (string.IsNullOrWhiteSpace(resquest.Details))
        {
            throw new PassInException("The Details is invalid");
        }
    }
}
