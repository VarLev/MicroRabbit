using System.Reflection;
using MediatR;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BankingDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbConnection"));
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo(){Title = "Banking Microservice", Version = "v1"});
});


builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

DependencyContainer.RegisterServices(builder.Services);


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

