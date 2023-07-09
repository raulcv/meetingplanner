using meetingplanner.app.Data;
using Microsoft.EntityFrameworkCore;

namespace meetingplanner.app.DataLoader
{
  public class SpeakerByIdDataLoader : BatchDataLoader<int, Speaker>
  {
    private readonly IDbContextFactory<AppDbContext> _dbContextFcatory;
    public SpeakerByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<AppDbContext> dbContextFactory) : base(batchScheduler)
    {
      _dbContextFcatory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync(
      IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
      await using AppDbContext dbContext = _dbContextFcatory.CreateDbContext();
      return await dbContext.Speakers
        .Where(s => keys.Contains(s.Id))
        .ToDictionaryAsync(t =>t.Id, cancellationToken);
    }
  }
}