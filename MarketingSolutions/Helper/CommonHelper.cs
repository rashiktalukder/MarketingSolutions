namespace MarketingSolutions.Helper
{
    public static class CommonHelper
    {
        public static bool IsUserAuthenticated(HttpContext httpContext)
        {
            return httpContext.Session.GetString("UserId") != null;
        }
        public static string GetUserId(HttpContext httpContext)
        {
            return httpContext?.Session?.GetString("UserId");
        }
        public static string GetUsername(HttpContext httpContext)
        {
            return httpContext?.Session?.GetString("Username");
        }
    }
}
