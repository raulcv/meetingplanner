using System.Collections.Generic;
using meetingplanner.app.Common;
using meetingplanner.app.Data;

namespace meetingplanner.app.Tracks
{
  public class TrackPayloadBase : Payload
  {
    public TrackPayloadBase(Track track)
    {
      Track = track;
    }
    public TrackPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
    public Track? Track { get; }
  }
}