using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using BurgerMasters.Core.Services;
using BurgerMasters.Extensions;
using BurgerMasters.Hubs;
using BurgerMasters.Infrastructure.Data;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BurgerMastersDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.ConfigureIdentity();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization header using the Bearer scheme (\"bearer {token} \")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//JWT SetUp
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
//SignalR SetUp
builder.Services.AddSignalR();
//Cors SetUp
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            //Inclueds nested collection in the response 
            options.JsonSerializerOptions.IncludeFields = true;
        });

//Prevent from CSRF
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "MyApp_CSRF";
    options.FormFieldName = "MyApp_CSRF_TOKEN";
    options.HeaderName = "X-CSRF-TOKEN";
});

builder.Services.AddHttpContextAccessor();
//All custom services
builder.Services.AddCustomServices();
//Set memory cache
builder.Services.AddMemoryCache();

//Prevents from returning automatically 400 Bad Request after failing Model validation!!!
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email Sender by Stiliyan Nikolov");
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BurgerMastersDbContext>();
    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ReviewHub>("/hubs/review");

app.MapControllers();

app.Run();