using Blocks.MediatR.Behaviours;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Application.Features.CreateArticle;
using System.Reflection;

namespace Submission.Application;

/// <summary>
/// Service registration helpers for the Submission application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application services including FluentValidation validators and MediatR
    /// with the validation and user id pipeline behaviors.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Application configuration (reserved for future use).</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>() // Register Fluent validators as transcient
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                config.AddOpenBehavior(typeof(SetUserIdBehaviour<,>));
            });

        return services;
    }
}
