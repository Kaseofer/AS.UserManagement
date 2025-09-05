using AgendaSaludApp.Application.IOC;
using AgendaSaludApp.Application.Mappers;
using AgendaSaludApp.Infrastructure.IOC;

using AgendaSaludApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using AgendaSaludApp.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaSaludDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AgendaSaludDb"))
    .UseSnakeCaseNamingConvention());


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationLayer();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
