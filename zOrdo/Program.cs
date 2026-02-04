using zOrdo.Repositories;
using zOrdo.Repositories.UsersRepository;
using zOrdo.Services.UserService;

Console.WriteLine("***********************************************************************************");
Console.WriteLine("***** Welcome To zOrdo - your AI-assisted task scheduling and planning engine *****");
Console.WriteLine("***********************************************************************************");
Console.WriteLine("");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Register DB utils
var connectionString = builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value ??
                       throw new InvalidOperationException("Missing DB connection string");
builder.Services.AddSingleton<ISharedDatabaseUtils>(new SharedDatabaseUtils(connectionString));

// Add services to the container
builder.Services.AddTransient<IUserService, UserService>();

// Add repositories to the container
builder.Services.AddTransient<IUserRepository, UserClient>();

builder.Services.AddHttpClient<IUserRepository, UserClient>(client =>
{
    var baseUrl = builder.Configuration["DatabaseProxy:BaseUrl"] ??
                  throw new InvalidOperationException("Missing DatabaseApi BaseUrl");
    
    client.BaseAddress = new Uri(baseUrl);
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();