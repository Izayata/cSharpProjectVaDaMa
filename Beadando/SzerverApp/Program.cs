using Microsoft.EntityFrameworkCore;
using Serilog;
using SzerverApp;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(
    (_, loggerConfiguration) => loggerConfiguration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BeadandoContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
        options.UseLazyLoadingProxies();
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
