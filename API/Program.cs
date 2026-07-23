using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add Application Layer
builder.Services.AddApplication();


// Add Infrastructure Layer
builder.Services.AddInfrastructure(
    builder.Configuration);


// Controllers
builder.Services.AddControllers();


// AutoMapper (AutoMapper 13+)
builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies());


// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter JWT token like: Bearer {your token}"
        });


    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});


// CORS (optional for Angular/React frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();


// Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseCors("AllowAll");


// Global Exception Middleware
app.UseMiddleware<
    API.Middleware.ExceptionMiddleware>();


// Authentication & Authorization
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();