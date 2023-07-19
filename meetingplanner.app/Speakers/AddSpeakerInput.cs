namespace meetingplanner.app.Domain
{
  public record AddSpeakerInput(
      string Name,
      string? Bio,
      string? WebSite);
}