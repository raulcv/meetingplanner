using meetingplanner.meetingplanner.app.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlite("Data Source=meeting.db"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
