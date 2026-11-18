using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using school.bootstrap;
using school.Data;
using school.Services;
using school.Services.Iservices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSeeding((dbContext, __) =>
    {
        SuperAdminSeeder.SeedSuperAdmin(dbContext, builder.Configuration);
    })
    .UseAsyncSeeding(
        async (dbContext, __, cancellationToken) =>
        {
            await SuperAdminSeeder.SeedSuperAdminAsync(dbContext, builder.Configuration, cancellationToken);
        }
    );
});

builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJwt, JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience=true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime=true,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token"]!)),
        ValidateActor=true

    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
