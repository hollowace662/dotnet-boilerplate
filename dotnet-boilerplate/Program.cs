using dotnet_boilerplate.Data;
using dotnet_boilerplate.Repositories;
using dotnet_boilerplate.Services;
using dotnet_boilerplate.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Services for OpenAPI spec usage
builder.Services.AddOpenApi();

//Add services to handle controllers
builder.Services.AddControllers();

//Add database context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

//Dependency injection for validators
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRoleRequestValidator>();

//Dependency injection for repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Dependency injection for services
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context
            .ModelState.Where(x => x.Value!.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var result = new
        {
            code = "VALIDATION_ERROR",
            message = "Validation failed",
            errors = errors,
        };

        return new BadRequestObjectResult(result);
    };
});

//JWT bearer authentication configuration
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://keycloak/auth/realms/mi-realm";
        options.Audience = "mi-api";
        options.RequireHttpsMetadata = true;
    });

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
