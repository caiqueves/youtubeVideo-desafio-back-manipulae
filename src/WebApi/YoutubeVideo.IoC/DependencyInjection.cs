using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using YoutubeVideo.Application.Interfaces;
using YoutubeVideo.Application.Services;
using YoutubeVideo.Data;
using YoutubeVideo.Data.Repositories;
using YoutubeVideo.Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using YoutubeVideo.Shareable.Validators;
using Microsoft.AspNetCore.Identity;
using YoutubeVideo.Shareable.DTOs;

namespace YoutubeVideo.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, string dbPath)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=c:\\temp\\Videos.sqlite"));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IVideoRepository, VideoRepository>();

        services.AddHttpClient();

        services.AddScoped<IVideoService, VideoService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "YouTube Video API",
                Version = "v1",
                Description = "API para manipulação de vídeos do YouTube",
            });

            // Configuração de segurança se necessário
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        });

        services.AddScoped<IValidator<VideoDto>, VideoDtoValidator>();
        services.AddScoped<IValidator<VideoFilterDto>, VideoFilterDtoValidator>();
        services.AddScoped<IValidator<BuscarIdDTO>, BuscarIdDtoValidator>();
        return services;
    }
}
