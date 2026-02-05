using Serilog;
using zOrdo.Repositories;
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
var connectionString =
    builder.Configuration.GetSection("Database:SQLite:ConnectionString").Value ??
    throw new InvalidOperationException("Missing DB connection string"); // TODO: default config
builder.Services.AddSingleton<ISharedDatabaseUtils>(new SharedDatabaseUtils(connectionString));

// Add repositories to the container
builder.Services.AddTransient<IUserRepository, UserRepository>();

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