using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using meetingplanner.app.Data;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using meetingplanner.app.DataLoader;

namespace meetingplanner.app.Sessions
{
    [ExtendObjectType("Query")]
    public class SessionQueries
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [ScopedService] AppDbContext context,
            CancellationToken cancellationToken) =>
            await context.Sessions.ToListAsync(cancellationToken);

        public Task<Session> GetSessionByIdAsync(
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            await sessionById.LoadAsync(ids, cancellationToken);
    }
}