using meetingplanner.app.Data;
using Microsoft.EntityFrameworkCore;

namespace meetingplanner.app.DataLoader
{
  public class SessionByIdDataLoader : BatchDataLoader<int, Session>
  {
    private readonly IDbContextFactory<AppDbContext> _dbContextFcatory;
    public SessionByIdDataLoader(
      IBatchScheduler batchScheduler,
      IDbContextFactory<AppDbContext> dbContextFactory) : base(batchScheduler)
    {
      _dbContextFcatory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Session>> LoadBatchAsync(
      IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
      await using AppDbContext dbContext = _dbContextFcatory.CreateDbContext();
      return await dbContext.Sessions
        .Where(s => keys.Contains(s.Id))
        .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
  }
}