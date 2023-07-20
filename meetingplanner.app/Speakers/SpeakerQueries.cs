using System.Linq;
using HotChocolate;
using meetingplanner.app.Data;
using meetingplanner.app.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace meetingplanner.app.Speakers
{
  [ExtendObjectType("Query")]
  public class SpeakerQueries
  {
    // public IQueryable<Speaker> GetSpeakers([Service] AppDbContext context) => context.Speakers; The OLD type
    [UseApplicationDbContext]
    public Task<List<Speaker>> GetSpeakersAsync([ScopedService] AppDbContext context) =>
      context.Speakers.ToListAsync();
    public Task<Speaker> GetSpeakerByIdAsync(
      [ID(nameof(Speaker))] int id,
      SpeakerByIdDataLoader dataLoader,
      CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
        [ID(nameof(Speaker))] int[] ids,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) =>
        await dataLoader.LoadAsync(ids, cancellationToken);

    public List<Speaker> GetSpeakersTest()
    {
      List<Session> sessions = new List<Session>{
      new Session{Id=1, Title="Session Title", StartTime=Convert.ToDateTime(DateTime.Now), EndTime=Convert.ToDateTime(DateTime.Now),
      SessionSpeakers={new SessionSpeaker{SessionId=1, SpeakerId=1}}
      }
      };

      List<Speaker> speakers = new List<Speaker>{
      new Speaker {Id=1, Name="Test speaker", Bio="Speakers Test Bio", WebSite= "URL",
      SessionSpeakers= new List<SessionSpeaker>{
        new SessionSpeaker{SessionId=1,
          Session= new Session{Id=1, Title="Session Title", StartTime=Convert.ToDateTime(DateTime.Now), EndTime=Convert.ToDateTime(DateTime.Now)}}}
      // SessionSpeakers={new SessionSpeaker{SpeakerId=1, SessionId=1}}
      }
      };
      foreach (var item in speakers)
      {
        Console.WriteLine(item.Name);
        Console.WriteLine(item.SessionSpeakers.ToList()[0].SessionId);
      }
      // await Task.Delay(100);
      return speakers;
    }

  }
}