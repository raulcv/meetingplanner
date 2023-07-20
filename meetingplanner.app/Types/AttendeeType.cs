using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using meetingplanner.app.Data;
using meetingplanner.app.DataLoader;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace meetingplanner.app.Types
{
  public class AttendeeType : ObjectType<Attendee>
  {
    protected override void Configure(IObjectTypeDescriptor<Attendee> descriptor)
    {
      descriptor
          .ImplementsNode()
          .IdField(t => t.Id)
          .ResolveNode((ctx, id) => ctx.DataLoader<AttendeeByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

      descriptor
          .Field(t => t.SessionsAttendees)
          .ResolveWith<AttendeeResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
          .UseDbContext<AppDbContext>()
          .Name("sessions");
    }

    private class AttendeeResolvers
    {
      public async Task<IEnumerable<Session>> GetSessionsAsync(
          Attendee attendee,
          [ScopedService] AppDbContext dbContext,
          SessionByIdDataLoader sessionById,
          CancellationToken cancellationToken)
      {
        int[] speakerIds = await dbContext.Attendees
            .Where(a => a.Id == attendee.Id)
            .Include(a => a.SessionsAttendees)
            .SelectMany(a => a.SessionsAttendees.Select(t => t.SessionId))
            .ToArrayAsync();

        return await sessionById.LoadAsync(speakerIds, cancellationToken);
      }
    }
  }
}