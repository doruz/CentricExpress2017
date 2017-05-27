using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoSecurity.Security
{
    public class TokenAuthorizationAttribute : ActionFilterAttribute
    {
        private const string TokenHeaderName = "token";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tokenHeader = context.HttpContext.Request.Headers[TokenHeaderName];
            var encryptedToken = tokenHeader.FirstOrDefault();

            if (!encryptedToken.IsTokenValid())
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
