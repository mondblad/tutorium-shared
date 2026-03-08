namespace Tutorium.Shared.Sessions.Const
{
    internal static class SessionConstants
    {
        public const string SessionPrefix = "sess:";
        public const string UserSessionsPrefix = "userSessions:";
        public static readonly TimeSpan Ttl = TimeSpan.FromDays(7);
    }
}
