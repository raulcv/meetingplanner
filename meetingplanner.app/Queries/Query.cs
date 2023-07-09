using System.Linq;
using HotChocolate;
using meetingplanner.app.Data;
using meetingplanner.app.Extensions;
using Microsoft.EntityFrameworkCore;

namespace meetingplanner.app.Queries
{
  public class Query
  {
    // public IQueryable<Speaker> GetSpeakers([Service] AppDbContext context) => context.Speakers; The OLD type
    [UseApplicationDbContext]
    [Obsolete]
    public Task<List<Speaker>> GetSpeakers([ScopedService] AppDbContext context) => context.Speakers.ToListAsync();
  }
}