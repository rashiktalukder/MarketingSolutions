namespace MarketingSolutions.Helper
{
    public static class CommonHelper
    {
        public static bool IsUserAuthenticated(HttpContext httpContext)
        {
            return httpContext.Session.GetString("UserId") != null;
        }
        public static string GetUsername(HttpContext httpContext)
        {
            var xxx = httpContext?.Session?.GetString("Username");
            return httpContext?.Session?.GetString("Username");
        }
    }
}
