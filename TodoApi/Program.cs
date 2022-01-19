using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TodoApi.Models;
using TodoApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApi.Controllers;


DataManager.StringToDateTime();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IServiceCollection serviceCollection = builder.Services.AddHostedService<BackgroundRoutine>();
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=poetry_database;Trusted_Connection=True"));

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

