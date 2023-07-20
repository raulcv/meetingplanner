using System.Collections.Generic;
using meetingplanner.app.Common;
using meetingplanner.app.Data;

namespace meetingplanner.app.Tracks
{
  public class RenameTrackPayload : TrackPayloadBase
  {
    public RenameTrackPayload(Track track)
        : base(track)
    {
    }
    public RenameTrackPayload(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
  }
}