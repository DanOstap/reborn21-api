using Microsoft.EntityFrameworkCore;
using Reborn;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("pg");
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
