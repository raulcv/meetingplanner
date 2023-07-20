using meetingplanner.app.Common;
using meetingplanner.app.Data;

namespace meetingplanner.app.Sessions
{
  public class AddSessionPayload : SessionPayloadBase
  {
    public AddSessionPayload(UserError error)
        : base(new[] { error })
    {
    }
    public AddSessionPayload(Session session) : base(session)
    {
    }
    public AddSessionPayload(IReadOnlyList<UserError> errors) : base(errors)
    {
    }
  }
}