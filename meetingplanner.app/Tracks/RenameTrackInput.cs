using meetingplanner.app.Data;
using HotChocolate.Types.Relay;

namespace meetingplanner.app.Tracks
{
  public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
}