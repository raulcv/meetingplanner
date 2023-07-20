using System.Threading;
using System.Threading.Tasks;
using meetingplanner.app.Data;
using HotChocolate;
using HotChocolate.Types;

namespace meetingplanner.app.Tracks
{
  [ExtendObjectType("Mutation")]
  public class TrackMutations
  {
    [UseApplicationDbContext]
    public async Task<AddTrackPayload> AddTrackAsync(
        AddTrackInput input,
        [ScopedService] AppDbContext context,
        CancellationToken cancellationToken)
    {
      var track = new Track { Name = input.Name };
      context.Tracks.Add(track);

      await context.SaveChangesAsync(cancellationToken);

      return new AddTrackPayload(track);
    }

    [UseApplicationDbContext]
    public async Task<RenameTrackPayload> RenameTrackAsync(
      RenameTrackInput input,
      [ScopedService] AppDbContext context,
      CancellationToken cancellationToken)
    {
      Track track = await context.Tracks.FindAsync(input.Id);
      track.Name = input.Name;

      await context.SaveChangesAsync(cancellationToken);

      return new RenameTrackPayload(track);
    }
  }
}