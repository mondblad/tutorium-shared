using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Tutorium.Shared.Sessions.Abstractions;
using Tutorium.Shared.Sessions.Infrastructure;
using Tutorium.Shared.Sessions.Services;
using Tutorium.Shared.Sessions.Infrastructure.Repositories;

namespace Tutorium.Shared.Sessions
{
    public static class SessionModule
    {
        private const string RedisConnectionStringName = "RedisSession";

        public static IServiceCollection AddSessionModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionRedisString = configuration.GetConnectionString(RedisConnectionStringName);
            if (string.IsNullOrWhiteSpace(connectionRedisString))
                throw new InvalidOperationException($"{RedisConnectionStringName} connection string is not configured");

            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionRedisString));

            services.AddScoped<IUserSessionRepository, RedisUserSessionRepository>();
            services.AddScoped<ISessionRepository, RedisSessionRepository>();
            services.AddScoped<ISessionIdGenerator, SessionIdGenerator>();
            services.AddScoped<ISessionUnitOfWork, SessionUnitOfWork>();
            services.AddScoped<ISessionManager, SessionManager>();

            return services;
        }
    }
}
