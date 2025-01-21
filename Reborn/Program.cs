
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Reborn;
using Reborn.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

string? connection = config.GetConnectionString("pg");

builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IOrderService, OrdersService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFileService, FileService>();

var jwtIssuer = config.GetSection("JWT:Issuer").Get<string>();
var jwtKey = config.GetSection("JWT:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("*") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseSwagger();
app.UseSwaggerUI();

app.UseCookiePolicy();
app.UseAuthorization();
app.MapControllers();
app.Run();
