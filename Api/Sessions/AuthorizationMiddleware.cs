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
            var endpoint = context.GetEndpoint();

            var requireUser = endpoint?.Metadata.GetMetadata<AllowUnauthenticatedAttribute>();

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
