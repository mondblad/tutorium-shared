using Microsoft.AspNetCore.Mvc;

namespace Tutorium.Shared.Api.Sessions
{
    public static class SessionControllerExtensions
    {
        public static int? GetCurrentUserId(this ControllerBase controller)
        {
            if (controller.Request.Headers.TryGetValue(SessionConstants.HeaderUserIdName, out var userIdValue) 
                && int.TryParse(userIdValue, out var userId))
            {
                return userId;
            }

            return null;
        }

        public static string? GetCurrentSessionId(this ControllerBase controller)
        {
            if (controller.Request.Headers.TryGetValue(SessionConstants.HeaderSessionIdName, out var sessionId))
                return sessionId;

            if (controller.Request.Cookies.TryGetValue(SessionConstants.SessionCookiesName, out var cookieSessionId))
                return cookieSessionId;

            return null;
        }
    }
}
