using System.Linq;
using HotChocolate;
using meetingplanner.app.Data;

namespace meetingplanner.app.Queries
{
  public class Query
  {
    public IQueryable<Speaker> GetSpeakers([Service] AppDbContext context) => context.Speakers;
  }
}