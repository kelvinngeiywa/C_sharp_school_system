using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using school.bootstrap;
using school.Data;
using school.Models.DTos;
using school.Services;
using school.Services.Iservices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value!.Errors.Count > 0)
        .ToDictionary(
            e => e.Key,
            e => e.Value!.Errors.Select(x=>x.ErrorMessage).ToArray()
            );

        var response = new ResponseDto(Success:false, Message:"Validation failed",Data:errors);
        return new BadRequestObjectResult(response);
    };
});
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
builder.Services.AddScoped<ITeacher, TeacherService>();
builder.Services.AddScoped<ISubject,SubjectService>();
builder.Services.AddScoped<IGuardian,GuardianService>();
builder.Services.AddScoped<IStudent, StudentService>();

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
