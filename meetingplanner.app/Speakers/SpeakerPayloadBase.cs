using System.Collections.Generic;
using meetingplanner.app.Common;
using meetingplanner.app.Data;

namespace meetingplanner.app.Speakers
{
  public class SpeakerPayloadBase : Payload
  {
    protected SpeakerPayloadBase(Speaker speaker)
    {
      Speaker = speaker;
    }
    protected SpeakerPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
    {
    }
    public Speaker? Speaker { get; }
  }
}