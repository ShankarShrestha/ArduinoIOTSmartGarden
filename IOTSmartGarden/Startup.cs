using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Notifications;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace IOTSmartGarden
{
    public class Startup
    {
        static string clientId = System.Configuration.ConfigurationManager.AppSettings["clientId"];

        static string redirectUrl = System.Configuration.ConfigurationManager.AppSettings["redirectUrl"];

        static string tenant = System.Configuration.ConfigurationManager.AppSettings["Tenant"];

        // This will need to be changed
        static string authority = String.Format(System.Globalization.CultureInfo.InvariantCulture, "https://login.microsoftonline.com/e04b402a-cd4a-47f5-ab4c-8d132f96d81f");

        /// <param name="app"></param>
        
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    RedirectUri = redirectUrl,
                    PostLogoutRedirectUri = redirectUrl,
                    Scope = OpenIdConnectScope.OpenIdProfile,
                    ResponseType = OpenIdConnectResponseType.IdToken,

                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false
                    },

                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthenticationFailed = OnAuthenticationFailed
                    }

                }
             );
        }


        /// <param name="context"></param>
        /// <returns></returns>

        private Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
        {
            context.HandleResponse();
            context.Response.Redirect("/?errormessage=" + context.Exception.Message);
            return Task.FromResult(0);
        }
    }
}