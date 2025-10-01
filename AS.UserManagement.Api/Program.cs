using AS.UserManagement.API.IOC;
using AS.UserManagement.Application.IOC;
using AS.UserManagement.Application.Mappers;
using AS.UserManagement.Infrastructure.IOC;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Adding configuration...");
builder.Configuration.AddEnvironmentVariables();

// Configurar URLs ANTES de build (solo para producción)
if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
    Console.WriteLine($"Production: Configured for port {port}");
}

Console.WriteLine("Registering services...");


builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddPresentationLayerService(builder.Configuration);

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
builder.Logging.AddConsole();
builder.Logging.AddDebug();

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
/*var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{port}");*/


app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
