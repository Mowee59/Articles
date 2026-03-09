using Articles.Abstractions.Enums;
using Microsoft.AspNetCore.Builder;


namespace Articles.Security;

/// <summary>
/// Extension methods for configuring authorization on minimal API endpoints.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Requires that the endpoint can only be accessed by users in any of the specified roles.
    /// </summary>
    /// <typeparam name="TBuilder">Type of the endpoint convention builder.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="roles">Role names required to access the endpoint.</param>
    /// <returns>The same endpoint convention builder for chaining.</returns>
    public static TBuilder RequireRoleAuthorization<TBuilder>(this TBuilder builder, params string[] roles)
        where TBuilder : IEndpointConventionBuilder
        => builder.RequireAuthorization(policy => policy.RequireRole(roles));

    /// <summary>
    /// Requires that the endpoint can only be accessed by users in any of the specified
    /// <see cref="UserRoleType"/> values. The enum values are converted to role names.
    /// </summary>
    /// <typeparam name="TBuilder">Type of the endpoint convention builder.</typeparam>
    /// <param name="builder">The endpoint convention builder.</param>
    /// <param name="roles">Enum-based roles required to access the endpoint.</param>
    /// <returns>The same endpoint convention builder for chaining.</returns>
    public static TBuilder RequireRoleAuthorization<TBuilder>(this TBuilder builder, params UserRoleType[] roles)
        where TBuilder : IEndpointConventionBuilder
        => builder.RequireAuthorization(policy => policy.RequireRole(string.Join(",", roles)));
}
