namespace meetingplanner.app.Domain
{
  public record SpeakerInput(
      string Name,
      string? Bio,
      string? WebSite);
}