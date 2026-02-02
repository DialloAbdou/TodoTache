using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.EndPoints;
using TodoList.Services;

var builder = WebApplication.CreateBuilder();

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
