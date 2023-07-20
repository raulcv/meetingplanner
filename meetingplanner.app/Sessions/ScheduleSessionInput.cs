using System;
using meetingplanner.app.Data;
using HotChocolate.Types.Relay;

namespace meetingplanner.app.Sessions
{
  public record ScheduleSessionInput(
      [ID(nameof(Session))]
        int SessionId,
      [ID(nameof(Track))]
        int TrackId,
      DateTimeOffset StartTime,
      DateTimeOffset EndTime);
}