using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.Text;
using Task_API.Data;
using Task_API.Services.Implementations;
using Task_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));
builder.Services.AddScoped<IPermissionServices,PermissionServices>();
builder.Services.AddScoped<IRoleServices,RoleServices>();
builder.Services.AddScoped<IUserServices,UserServices>();
builder.Services.AddScoped<IRolePermissionServices,RolePermissionServices>();
builder.Services.AddScoped<ITaskStatusServices,TaskStatusServices>();
builder.Services.AddScoped<ITaskServices,TaskServices>();
builder.Services.AddScoped<IUserLoginServices,UserLoginServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUIOrigin",
        policy => policy
            .WithOrigins("http://localhost:5093")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// JWT Setup
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowUIOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
