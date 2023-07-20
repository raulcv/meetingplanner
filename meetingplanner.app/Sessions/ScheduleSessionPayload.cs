using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using meetingplanner.app.Common;
using meetingplanner.app.Data;
using meetingplanner.app.DataLoader;
using HotChocolate;

namespace meetingplanner.app.Sessions
{
  public class ScheduleSessionPayload : SessionPayloadBase
  {
    public ScheduleSessionPayload(Session session)
        : base(session)
    {
    }

    public ScheduleSessionPayload(UserError error)
        : base(new[] { error })
    {
    }

    public async Task<Track?> GetTrackAsync(
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken)
    {
      if (Session is null)
      {
        return null;
      }

      return await trackById.LoadAsync(Session.Id, cancellationToken);
    }

    [UseApplicationDbContext]
    public async Task<IEnumerable<Speaker>?> GetSpeakersAsync(
        [ScopedService] AppDbContext dbContext,
        SpeakerByIdDataLoader speakerById,
        CancellationToken cancellationToken)
    {
      if (Session is null)
      {
        return null;
      }

      int[] speakerIds = await dbContext.Sessions
          .Where(s => s.Id == Session.Id)
          .Include(s => s.SessionSpeakers)
          .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
          .ToArrayAsync();

      return await speakerById.LoadAsync(speakerIds, cancellationToken);
    }
  }
}