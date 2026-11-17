using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using school.bootstrap;
using school.Data;
using school.Services;
using school.Services.Iservices;

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
