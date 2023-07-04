
using meetingplanner.app.Data;

namespace meetingplanner.app.Domain
{
  public class SpeakerOutput
  {
    public SpeakerOutput(Speaker speaker)
    {
      Speaker = speaker;
    }
    public Speaker Speaker { get; }
  }
}