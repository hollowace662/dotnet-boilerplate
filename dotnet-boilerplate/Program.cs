using dotnet_boilerplate.Data;
using dotnet_boilerplate.Repositories;
using dotnet_boilerplate.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Services for OpenAPI spec usage
builder.Services.AddOpenApi();

//Add services to handle controllers
builder.Services.AddControllers();

//Add database context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

//Dependency injection for repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Dependency injection for services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

//If https is enabled, redirect http requests to https
app.UseHttpsRedirection();

//Enable OpenAPI spec and Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Map ControllerBase implementations to routes
app.MapControllers();

app.Run();
