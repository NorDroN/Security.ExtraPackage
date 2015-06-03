// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.Authentication.VK
{
    internal class VKAuthenticationHandler : OAuthAuthenticationHandler<VKAuthenticationOptions, IVKAuthenticationNotifications>
    {
        public VKAuthenticationHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> GetUserInformationAsync(AuthenticationProperties properties, TokenResponse tokens)
        {
            var userid = tokens.Response.Value<string>("user_id");
            var email = tokens.Response.Value<string>("email");

            var graphAddress = Options.UserInformationEndpoint +
                                      "?user_ids=" + UrlEncoder.UrlEncode(userid) +
                                      "&fields=" + UrlEncoder.UrlEncode("screen_name");

            var graphResponse = await Backchannel.GetAsync(graphAddress, Context.RequestAborted);
            graphResponse.EnsureSuccessStatusCode();
            var text = await graphResponse.Content.ReadAsStringAsync();
            var user = GetUserFromXml(text);
            user.Add("email", email);

            var context = new VKAuthenticatedContext(Context, Options, user, tokens);
            var identity = new ClaimsIdentity(
                Options.ClaimsIssuer,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            if (!string.IsNullOrEmpty(context.Id))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.Email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, context.Email, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.FirstName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, context.FirstName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.LastName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, context.LastName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.ScreenName))
            {
                // Many VK accounts do not set the ScreenName field.
                identity.AddClaim(new Claim("urn:vk:screenname", context.ScreenName, ClaimValueTypes.String, Options.ClaimsIssuer));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.ScreenName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.Name) && string.IsNullOrEmpty(context.ScreenName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, context.Name, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            context.Properties = properties;
            context.Principal = new ClaimsPrincipal(identity);

            await Options.Notifications.Authenticated(context);

            return new AuthenticationTicket(context.Principal, context.Properties, context.Options.AuthenticationScheme);
        }

        private JObject GetUserFromXml(string xml)
        {
            var doc = JObject.Parse(xml)["response"][0].ToString();
            return JObject.Parse(doc);
        }
    }
}