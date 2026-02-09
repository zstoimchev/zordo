using Serilog;
using zOrdo.Middleware;
using zOrdo.Repositories.TodoItemRepository;
using zOrdo.Repositories.UsersRepository;
using zOrdo.Services.TodoItemService;
using zOrdo.Services.UserService;

Console.WriteLine("***********************************************************************************");
Console.WriteLine("***** Welcome To zOrdo - your AI-assisted task scheduling and planning engine *****");
Console.WriteLine("***********************************************************************************");
Console.WriteLine("");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Add services to the container
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITodoItemService, TodoItemService>();

// Add repositories to the container
builder.Services.AddTransient<IUserRepository, UserClient>();
builder.Services.AddTransient<ITodoItemRepository, TodoItemClient>();

// Configure Http Clients
builder.Services.AddHttpClient("zOrdo.DatabaseApi", client =>
{
    var baseUrl =
        builder.Configuration["Clients:DatabaseApi:BaseUrl"] ??
        throw new InvalidOperationException("Missing DatabaseApi BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();