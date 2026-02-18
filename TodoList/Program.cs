using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TodoList.Data;
using TodoList.EndPoints;
using TodoList.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var loggerConfiguration = new LoggerConfiguration();
loggerConfiguration.WriteTo.Console();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<ITacheService, TacheService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<TacheDbContext>(op=>op
                .UseSqlServer(builder.Configuration.GetConnectionString("SqlConnexion")));

var app = builder.Build();


app.MapGroup("/todos")
  .MapTacheRouteGroup();

app.MapGroup("/users")
    .MapUSerRouteGroup();
app.Run();
