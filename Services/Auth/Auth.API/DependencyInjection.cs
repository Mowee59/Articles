using Articles.Security;
using Auth.Domain.Users;
using Auth.Persistence;
using EmailService.Smtp;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auth.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // Use it for configuring the options 
        services.
            AddAndValidateOptions<JwtOptions>(configuration);

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddJwtAuthentication(config)
            .AddJwtIdentity(config)
            .AddAuthorization();

        services.AddSmtpEmailService(config);

        return services;
    }

    public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration config )
    {

        services.AddIdentityCore<User>(options =>
        {
            //Lockout settings
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            // options.User0Allowe


        })
        .AddRoles<Auth.Domain.Roles.Role>()
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddSignInManager<SignInManager<User>>()
        .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
        });

        return services;

    }
}
