using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Services;
using BurgerMasters.Extensions;
using BurgerMasters.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BurgerMastersDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.ConfigureIdentity();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin", 
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

//Prevents from returning automatically 400 Bad Request after failing Model validation!!!
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
