// Helpers/SessionAuthorizeAttribute.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_Novel.Helpers
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userId = session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                // redirect ไป Login พร้อมบอก returnUrl
                context.Result = new RedirectToActionResult(
                    "Login", "Account",
                    new { area = "", returnUrl = context.HttpContext.Request.Path });
            }

            base.OnActionExecuting(context);
        }
    }
}