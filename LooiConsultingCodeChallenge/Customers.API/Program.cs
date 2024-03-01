using Common.Infra;
using Customers.Application.Interfaces;
using Customers.Application.Services;
using Customers.Domain.Interfaces;
using Customers.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("./logs.txt")
    .CreateLogger();
builder.Services.AddSingleton<Serilog.ILogger>(log);
log.Information("Serilog added.");

// Add services to the container.
builder.Services
    .AddScoped<ICustomersRepository, CustomersRepository>()
    .AddScoped<ICustomersService, CustomersService>()
    .AddDbContext<EfContext>(opt => opt.UseInMemoryDatabase("dbTest"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var customerService = scope.ServiceProvider.GetRequiredService<ICustomersService>();
await customerService.GenerateMoqDataAsync();

app.Run();
