using Example.Persistence;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PubsDBFirst;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Get Configuration file
//IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
//IConfigurationRoot configuration = configurationBuilder.Build();
//string? testConfig = configuration.GetConnectionString("Herald");

// Activate DB Context
builder.Services.AddDbContext<GreenwayContext>(option => option.UseSqlServer(configuration.GetConnectionString("Herald")));
builder.Services.AddDbContext<PubsContext>(option => option.UseSqlServer(configuration.GetConnectionString("Pubs")));

// Config MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Add services to the container.

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

app.Run();
