using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using meetingplanner.app.Data;
using GreenDonut;

namespace meetingplanner.app.DataLoader
{
  public class TrackByIdDataLoader : BatchDataLoader<int, Track>
  {
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public TrackByIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<AppDbContext> dbContextFactory)
        : base(batchScheduler)
    {
      _dbContextFactory = dbContextFactory ??
          throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Track>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
      await using AppDbContext dbContext =
          _dbContextFactory.CreateDbContext();

      return await dbContext.Tracks
          .Where(s => keys.Contains(s.Id))
          .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
  }
}