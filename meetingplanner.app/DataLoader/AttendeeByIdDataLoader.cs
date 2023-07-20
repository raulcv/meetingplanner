using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreenDonut;
using meetingplanner.app.Data;

namespace meetingplanner.app.DataLoader
{
  public class AttendeeByIdDataLoader : BatchDataLoader<int, Attendee>
  {
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public AttendeeByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<AppDbContext> dbContextFactory)
        : base(batchScheduler)
    {
      _dbContextFactory = dbContextFactory ??
          throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
      await using AppDbContext dbContext =
          _dbContextFactory.CreateDbContext();

      return await dbContext.Attendees
          .Where(s => keys.Contains(s.Id))
          .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
  }
}