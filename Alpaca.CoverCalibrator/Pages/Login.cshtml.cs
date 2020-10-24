using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alpaca.CoverCalibrator.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword, string paramReturnUrl)
        {
            string returnUrl = null;

            if (Url.IsLocalUrl(paramReturnUrl))
            {
                returnUrl = Url.Content(paramReturnUrl);
            }
            else
            {
                returnUrl = Url.Content("~/");
            }
                

            try
            {
                // Clear the existing external cookie
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            bool auth = false;

            try
            {
                if (ServerSettings.UseAuth)
                {
                    auth = await _userService.Authenticate(paramUsername, paramPassword);
                }
                else
                {
                    auth = true;
                }
            }
            catch
            {
                return LocalRedirect(returnUrl);
            }

            if (!auth)
                return LocalRedirect(returnUrl);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername ?? "Default")
            };

            var claimsIdentity = new ClaimsIdentity(

                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = this.Request.Host.Value
            };

            try
            {
                await HttpContext.SignInAsync(

                CookieAuthenticationDefaults.AuthenticationScheme,

                new ClaimsPrincipal(claimsIdentity),

                authProperties);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return LocalRedirect(returnUrl);
        }
    }
}