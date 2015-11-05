using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authentication;
using System.Security.Claims;

namespace WebApp
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddConsole(LogLevel.Information);

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.LoginPath = new PathString("/login");
            });

            app.UseVKAuthentication(options =>
            {
                options.ClientId = "3876732";
                options.ClientSecret = "A2FEsmg4okMPNLY32H7S";
            });

            app.UseOdnoklassnikiAuthentication(options =>
            {
                options.ClientId = "194282240";
                options.ClientSecret = "92DB0969DE3D4BD6CA53550E";
                options.ClientPublicKey = "CBAEIFJMABABABABA";
            });

            app.UseLinkedInAuthentication(options =>
            {
                options.ClientId = "77pacfa9kbmrgy";
                options.ClientSecret = "068CQsV4iubzwpQ2";
            });

            app.UseInstagramAuthentication(options =>
            {
                options.ClientId = "aeb810cc6b8d41a98a6ffb5c5d533782";
                options.ClientSecret = "b760abd2ce3844d4b93bb5dcf9dc5f66";
            });

            app.UseFoursquareAuthentication(options =>
            {
                options.ClientId = "AZGRK3HBBHKLJLLVKG40ZDAGDQGW44DYHSOEJVSJVT0PGYWU";
                options.ClientSecret = "LHZ5VFQG4L5CB0WT4RONQEFMMZRWRODZQ1HKDQE5CGMS5WXU";
            });

            // Choose an authentication type
            app.Map("/login", signoutApp =>
            {
                signoutApp.Run(async context =>
                {
                    var authType = context.Request.Query["authscheme"];
                    if (!string.IsNullOrEmpty(authType))
                    {
                        // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
                        // send them to the home page instead (/).
                        await context.Authentication.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
                        return;
                    }

                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("<html><body>");
                    await context.Response.WriteAsync("Choose an authentication scheme: <br>");
                    foreach (var type in context.Authentication.GetAuthenticationSchemes())
                    {
                        await context.Response.WriteAsync("<a href=\"?authscheme=" + type.AuthenticationScheme + "\">" + (type.DisplayName ?? "(suppressed)") + "</a><br>");
                    }
                    await context.Response.WriteAsync("</body></html>");
                });
            });

            // Sign-out to remove the user cookie.
            app.Map("/logout", signoutApp =>
            {
                signoutApp.Run(async context =>
                {
                    await context.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("<html><body>");
                    await context.Response.WriteAsync("You have been logged out. Goodbye " + context.User.Identity.Name + "<br>");
                    await context.Response.WriteAsync("<a href=\"/\">Home</a>");
                    await context.Response.WriteAsync("</body></html>");
                });
            });

            // Deny anonymous request beyond this point.
            app.Use(async (context, next) =>
            {
                if (string.IsNullOrEmpty(context.User.Identity.Name))
                {
                    // The cookie middleware will intercept this 401 and redirect to /login
                    await context.Authentication.ChallengeAsync();
                    return;
                }
                await next();
            });

            // Display user information
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("Hello " + context.User.Identity.Name + "<br>");
                foreach (var claim in context.User.Claims)
                {
                    await context.Response.WriteAsync(claim.Type + ": " + claim.Value + "<br>");
                }
                await context.Response.WriteAsync("<a href=\"/logout\">Logout</a>");
                await context.Response.WriteAsync("</body></html>");
            });
        }
    }
}
