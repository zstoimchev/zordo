using zOrdo.Repositories;
using zOrdo.Repositories.UsersRepository;
using zOrdo.Services.UserService;

Console.WriteLine("***********************************************************************************");
Console.WriteLine("***** Welcome To zOrdo - your AI-assisted task scheduling and planning engine *****");
Console.WriteLine("***********************************************************************************");
Console.WriteLine("");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container
builder.Services.AddTransient<IUserService, UserService>();

// Add repositories to the container
builder.Services.AddTransient<IUserRepository, UserClient>();

// Configure Http Clients
builder.Services.AddHttpClient<IUserRepository, UserClient>(client =>
{
    var baseUrl = builder.Configuration["Clients:DatabaseApi:BaseUrl"] ??
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