using Microsoft.EntityFrameworkCore;
using System.Security;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUIOrigin",
        policy => policy
            .WithOrigins("http://localhost:5093")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
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

app.UseAuthorization();

app.MapControllers();

app.Run();
