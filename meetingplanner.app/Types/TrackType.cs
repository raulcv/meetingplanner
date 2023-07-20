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
  public class TrackType : ObjectType<Track>
  {
    protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
    {
      descriptor
          .ImplementsNode()
          .IdField(t => t.Id)
          .ResolveNode((ctx, id) =>
              ctx.DataLoader<TrackByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

      descriptor
          .Field(t => t.Sessions)
          .ResolveWith<TrackResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
          .UseDbContext<AppDbContext>()
          .Name("sessions");
    }

    private class TrackResolvers
    {
      public async Task<IEnumerable<Session>> GetSessionsAsync(
          Track track,
          [ScopedService] AppDbContext dbContext,
          SessionByIdDataLoader sessionById,
          CancellationToken cancellationToken)
      {
        int[] sessionIds = await dbContext.Sessions
            .Where(s => s.Id == track.Id)
            .Select(s => s.Id)
            .ToArrayAsync();

        return await sessionById.LoadAsync(sessionIds, cancellationToken);
      }
    }
  }
}