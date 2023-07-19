using System.Threading.Tasks;
using HotChocolate;
using meetingplanner.app.Data;
using meetingplanner.app.Domain;
using meetingplanner.app.Extensions;

namespace meetingplanner.app.Speakers
{
  [ExtendObjectType("Mutation")]
  public class SpeakerMutations
  {
    #region Old one to make Mutation query
    /*
    public async Task<SpeakerOutput> AddSpeakerAsync(SpeakerInput input, [Service] AppDbContext context)
    {
      var speaker = new Speaker
      {
        Name = input.Name,
        Bio = input.Bio,
        WebSite = input.WebSite
      };

      context.Speakers.Add(speaker);
      await context.SaveChangesAsync();

      return new SpeakerOutput(speaker);
    }
    */
    #endregion
    [UseApplicationDbContext]
    public async Task<AddSpeakerPayload> AddSpeakerAsync(
      AddSpeakerInput input,
      [ScopedService] AppDbContext context)
    {
      var speaker = new Speaker
      {
        Name = input.Name,
        Bio = input.Bio,
        WebSite = input.WebSite
      };

      context.Speakers.Add(speaker);
      await context.SaveChangesAsync();

      return new AddSpeakerPayload(speaker);
    }
  }
}