using meetingplanner.app.Data;
using meetingplanner.app.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace meetingplanner.app.Types
{
  public class SpeakerType : ObjectType<Speaker>
  {
    protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
    {
      descriptor
        .ImplementsNode()
        .IdField(t => t.Id)
        .ResolveNode((cxt, id) => cxt.DataLoader<SpeakerByIdDataLoader>().LoadAsync(id, cxt.RequestAborted));
      // descriptor.Field(t => t.Id).ID("FOO");

      descriptor
        .Field(t => t.SessionSpeakers)
        .ResolveWith<SpeakerResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
        .UseDbContext<AppDbContext>()
        .Name("sessions");
    }
    private class SpeakerResolvers
    {
      public async Task<IEnumerable<Session>> GetSessionsAsync(
         Speaker speaker,
         [ScopedService] AppDbContext dbContext,
         SessionByIdDataLoader sessionById,
         CancellationToken cancellationToken)
      {
        int[] sessionIds = await dbContext.Speakers
          .Where(s => s.Id == speaker.Id)
          .Include(s => s.SessionSpeakers)
          .SelectMany(s => s.SessionSpeakers.Select(t => t.SessionId))
          .ToArrayAsync();
        return await sessionById.LoadAsync(sessionIds, cancellationToken);
      }
    }
  }
}