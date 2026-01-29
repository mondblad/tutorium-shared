using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tutorium.Shared.Utils.Grpc
{
    public static class GrpcClientExtensions
    {
        public static void RegisterGrpcClient<TClient>(this WebApplicationBuilder builder, string serviceName) where TClient : class
        {
            builder.Services.Configure<GrpcServiceOptions>(serviceName, builder.Configuration.GetSection($"GrpcServices:{serviceName}"));
            builder.Services.RegisterGrpcClient<TClient>(serviceName);
        }

        private static void RegisterGrpcClient<TClient>(this IServiceCollection services, string serviceName) where TClient : class
        {
            services.AddGrpcClient<TClient>((sp, options) =>
            {
                var cfg = sp
                    .GetRequiredService<IOptionsMonitor<GrpcServiceOptions>>()
                    .Get(serviceName);

                options.Address = new Uri(cfg.Address);
            });
        }
    }
}
