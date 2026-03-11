using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence;

public static class DependencyConfiguration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config )
    {
        var connectionstring = config.GetConnectionString("Database");
        services.AddDbContext<AuthDbContext>(opts => opts.UseSqlServer(connectionstring));

        return services;

    }
}
