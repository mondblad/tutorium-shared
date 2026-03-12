using Grpc.AspNetCore.Server;
using Microsoft.AspNetCore.Http;

namespace Tutorium.Shared.Api.Sessions
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.StartsWithSegments("/swagger"))
            {
               await _next(context);
               return;
            }

            

            var endpoint = context.GetEndpoint();
            if (endpoint is null)
            {
                await _next(context);
                return;
            }

            if (endpoint.Metadata.GetMetadata<GrpcMethodMetadata>() != null)
            {
                await _next(context);
                return;
            }

            var requireUser = endpoint.Metadata.GetMetadata<AllowUnauthenticatedAttribute>();
            if (requireUser == null)
            {
                var userId = context.Request.Headers[SessionConstants.HeaderUserIdName];

                if (string.IsNullOrEmpty(userId))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }
}
