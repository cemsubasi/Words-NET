using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Words.Application.Abstractions;
using Words.Application.Services;
using Words.Application.Validations;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;
using Words.Infastructure.Authentication;
using Words.Persistence.Contexts;
using Words.Persistence.Extensions;
using Words.Persistence.Middlewares;
using Words.Persistence.Repositories;
using Words.Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddRepositories(ServiceLifetime.Transient);
builder.Services.AddServices(ServiceLifetime.Transient);
builder.Services.AddValidations(ServiceLifetime.Transient);
builder.Services.AddAuthentication(args);
builder.Services.AddSwagger();

builder.Services.AddDbContext<MainDbContext>();
builder.Services.AddAuthorization();
builder.Services.AddScoped<SessionMiddleware>();
builder.Services.ConfigureOptions<JwtOptionSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
//}

app.UseCors(builder =>
        builder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SessionMiddleware>();

app.MapControllers();

app.Run();
