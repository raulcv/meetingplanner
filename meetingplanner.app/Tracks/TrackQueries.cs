using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using meetingplanner.app.Data;
using meetingplanner.app.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace meetingplanner.app.Tracks
{
  [ExtendObjectType("Query")]
  public class TrackQueries
  {
    [UseApplicationDbContext]
    public async Task<IEnumerable<Track>> GetTracksAsync(
        [ScopedService] AppDbContext context,
        CancellationToken cancellationToken) =>
        await context.Tracks.ToListAsync(cancellationToken);

    [UseApplicationDbContext]
    public Task<Track> GetTrackByNameAsync(
        string name,
        [ScopedService] AppDbContext context,
        CancellationToken cancellationToken) =>
        context.Tracks.FirstAsync(t => t.Name == name);

    [UseApplicationDbContext]
    public async Task<IEnumerable<Track>> GetTrackByNamesAsync(
        string[] names,
        [ScopedService] AppDbContext context,
        CancellationToken cancellationToken) =>
        await context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync();

    public Task<Track> GetTrackByIdAsync(
        [ID(nameof(Track))] int id,
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken) =>
        trackById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Track>> GetTracksByIdAsync(
        [ID(nameof(Track))] int[] ids,
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken) =>
        await trackById.LoadAsync(ids, cancellationToken);
  }
}