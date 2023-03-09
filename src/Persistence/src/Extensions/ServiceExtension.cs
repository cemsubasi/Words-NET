using FluentValidation;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Words.Application.Abstractions;
using Words.Application.Services;
using Words.Application.Validations;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;
using Words.Infastructure.Authentication;
using Words.Persistence.Repositories;
using Words.Application.Helpers;

namespace Words.Persistence.Extensions;

public static class ServiceExtension {
  public static IServiceCollection AddRepositories(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), lifetime));
    services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>), lifetime));
    services.Add(new ServiceDescriptor(typeof(IUserRepository), typeof(UserRepository), lifetime));
    services.Add(new ServiceDescriptor(typeof(ICategoryRepository), typeof(CategoryRepository), lifetime));
    services.Add(new ServiceDescriptor(typeof(IWordRepository), typeof(WordRepository), lifetime));

    return services;
  }

  public static IServiceCollection AddServices(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(AuthService), typeof(AuthService), lifetime));
    services.Add(new ServiceDescriptor(typeof(UserService), typeof(UserService), lifetime));
    services.Add(new ServiceDescriptor(typeof(CategoryService), typeof(CategoryService), lifetime));
    services.Add(new ServiceDescriptor(typeof(WordService), typeof(WordService), lifetime));
    services.Add(new ServiceDescriptor(typeof(SessionService), typeof(SessionService), lifetime));

    return services;
  }

  public static IServiceCollection AddValidations(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IValidator<UserModel>), typeof(UserValidator), lifetime));
    services.Add(new ServiceDescriptor(typeof(IValidator<WordModel>), typeof(WordValidator), lifetime));
    services.Add(new ServiceDescriptor(typeof(IValidator<AnswerModel>), typeof(AnswerValidator), lifetime));
    services.Add(new ServiceDescriptor(typeof(IValidator<CategoryModel>), typeof(CategoryValidator), lifetime));

    return services;
  }

  public static IServiceCollection AddUtils(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IJwtProvider), typeof(JwtProvider), lifetime));
    services.Add(new ServiceDescriptor(typeof(IPasswordManager), typeof(PasswordManager), lifetime));

    return services;
  }

  public static AuthenticationBuilder AddAuthentication(this IServiceCollection services, string[] args) {
    return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => {
        options.TokenValidationParameters = JwtProvider.GetValidationParameters(args);
      });
  }

  public static IServiceCollection AddSwagger(this IServiceCollection services) {
    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen(options => {
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
      });
      options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
      });
    });

    return services;
  }
}
