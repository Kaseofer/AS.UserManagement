using AgendaSaludApp.Application.IOC;
using AgendaSaludApp.Application.Mappers;
using AgendaSaludApp.Application.Validators;
using AgendaSaludApp.Infrastructure.IOC;
using AgendaSaludApp.Infrastructure.Logger;
using AgendaSaludApp.Infrastructure.Persistence.Context;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaSaludDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AgendaSaludDb"))
    .UseSnakeCaseNamingConvention());


//Logger
builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(FileLogger<>));

builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationLayer();

// Registrás los validadores que tengas en tu proyecto
builder.Services.AddFluentValidationAutoValidation();



// Configuraciones de servicios de tu arquitectura limpia
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});



builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles).Assembly);

// Dependency Injection


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Para Railway - usar puerto dinámico
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{port}");


app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
