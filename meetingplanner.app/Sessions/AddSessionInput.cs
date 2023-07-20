using System.Collections.Generic;
using meetingplanner.app.Data;
using HotChocolate.Types.Relay;

namespace meetingplanner.app.Sessions
{
  public record AddSessionInput(
    string Title,
    string? Abstract,
    [ID(nameof(Speaker))]
    IReadOnlyList<int> SpeakerIds);
}