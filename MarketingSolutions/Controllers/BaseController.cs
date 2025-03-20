using MarketingSolutions.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketingSolutions.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // List of actions that don't require session checks
            var publicActions = new[] { "Login", "Register" }; // Add other public actions here

            // Get the current action name
            var actionName = context.ActionDescriptor.RouteValues["action"];

            // Skip session check for public actions
            if (publicActions.Contains(actionName))
            {
                return;
            }

            // Check if the session has expired
            if (!CommonHelper.IsUserAuthenticated(HttpContext))
            {
                // Clear session data and cookie
                HttpContext.Session.Clear();
                Response.Cookies.Delete(".AspNetCore.Session");

                // Redirect to login page
                context.Result = RedirectToAction("Login", "Account");
            }
        }
    }
}
