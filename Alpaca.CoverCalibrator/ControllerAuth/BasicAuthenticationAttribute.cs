
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Alpaca.CoverCalibrator
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public string BasicRealm { get; set; }

        IUserService userService;

        public BasicAuthenticationAttribute(IUserService _userService)
        {
            userService = _userService;
        }

        public async override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var endpoint = filterContext.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return;

            if (!ServerSettings.UseAuth)
            {
                return;
            }

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!string.IsNullOrEmpty(auth))
            {
                var authHeader = AuthenticationHeaderValue.Parse(auth);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                if (await userService.Authenticate(username, password))
                {
                    return;
                }
            }
            filterContext.HttpContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", BasicRealm ?? "Alpaca"));
            filterContext.Result = new UnauthorizedResult();
        }
    }
}
