using Serilog;
using zOrdo.Repositories;
using zOrdo.Repositories.TodoItemRepository;
using zOrdo.Repositories.UsersRepository;

Console.WriteLine("****************************************************");
Console.WriteLine("*****      zOrdo.DatabaseApi is starting      *****");
Console.WriteLine("****************************************************");
Console.WriteLine("");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();

// Register DB utils
builder.Services.AddSingleton<ISharedDatabaseUtils, SharedDatabaseUtils>();

// Add repositories to the container
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITodoItemRepository, TodoItemRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.MapControllers();
app.Run();