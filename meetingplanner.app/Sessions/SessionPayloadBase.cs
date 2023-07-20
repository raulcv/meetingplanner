using System.Collections.Generic;
using meetingplanner.app.Common;
using meetingplanner.app.Data;

namespace meetingplanner.app.Sessions
{
  public class SessionPayloadBase : Payload
  {
    protected SessionPayloadBase(Session session)
    {
      Session = session;
    }
    protected SessionPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
    public Session? Session { get; }
  }
}