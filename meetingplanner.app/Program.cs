using meetingplanner.app.Queries;
using meetingplanner.app.Data;
using Microsoft.EntityFrameworkCore;
using meetingplanner.app.Mutations;

var builder = WebApplication.CreateBuilder(args);
// var services = builder.Services;

// builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlite("Data Source=meeting.db")); For a instance for each request
builder.Services.AddPooledDbContextFactory<AppDbContext>(option => option.UseSqlite("Data Source=meeting.db"));
builder.Services
  // .RegisterDbContext<AppContext>(DbContextKind.Pooled)
  .AddGraphQLServer().AddQueryType<Query>()
  .AddGraphQLServer().AddMutationType<Mutation>();

var app = builder.Build();

app.UseRouting();

// app.UseEndpoints(endpoints =>
// {
//   endpoints.MapGraphQL();
// });
app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();
