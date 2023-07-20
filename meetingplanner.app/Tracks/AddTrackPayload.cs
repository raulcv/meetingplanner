using System.Collections.Generic;
using meetingplanner.app.Common;
using meetingplanner.app.Data;

namespace meetingplanner.app.Tracks
{
  public class AddTrackPayload : TrackPayloadBase
  {
    public AddTrackPayload(Track track)
        : base(track)
    {
    }
    public AddTrackPayload(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
  }
}