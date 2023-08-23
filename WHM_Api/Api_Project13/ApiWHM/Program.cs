using ApiWHM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WhmanagementContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("WHMConStr"))
    );
// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Profile));
builder.Services.AddCors();

var app = builder.Build();
app.UseExceptionHandler("/error");
app.UseCors(
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }
    );
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
