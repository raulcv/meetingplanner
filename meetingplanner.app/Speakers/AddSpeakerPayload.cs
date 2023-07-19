
using meetingplanner.app.Data;
using meetingplanner.app.Common;

namespace meetingplanner.app.Speakers
{
  public class AddSpeakerPayload : SpeakerPayloadBase
  {

    // public AddSpeakerPayload(Speaker speaker)
    // {
    //   Speaker = speaker;
    // }
    // public Speaker Speaker { get; }
    public AddSpeakerPayload(Speaker speaker) : base(speaker)
    {
    }
    public AddSpeakerPayload(IReadOnlyList<UserError> errors) : base(errors)
    {

    }
  }
}