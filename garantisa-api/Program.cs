using FluentValidation;
using FluentValidation.AspNetCore;
using garantisa.Data;
using garantisa.Repositories;
using garantisa.Services;
using garantisa.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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

//API Request validation
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
        options.Authority = builder.Configuration.GetValue<String>("Jwt:Authority");
        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudiences = builder.Configuration.GetSection("Jwt:Audiences").Get<string[]>(),
        };
    });

var serviceName = "garantisa-api";

builder
    .Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService(serviceName))
    .WithTracing(tracing =>
    {
        tracing
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://lgtm:4318/v1/traces");
                options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
            });
    });

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("garantisa-api"));
    logging.IncludeScopes = true;
    logging.IncludeFormattedMessage = true;
    logging.ParseStateValues = true;
    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://lgtm:4317");
        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
    });
});

var app = builder.Build();

//Enable OpenAPI spec and Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

//Map ControllerBase implementations to routes
app.MapControllers();

app.Run();
