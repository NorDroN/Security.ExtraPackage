// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.WebUtilities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Authentication.VK
{
    internal class VKAuthenticationHandler : OAuthAuthenticationHandler<VKAuthenticationOptions>
    {
        public VKAuthenticationHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var userid = tokens.Response.Value<string>("user_id");
            var email = tokens.Response.Value<string>("email");

            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, new Dictionary<string, string>
            {
                { "user_ids", userid },
                { "fields", "screen_name" }
            });

            var response = await Backchannel.GetAsync(endpoint, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var xml = await response.Content.ReadAsStringAsync();
            var payload = GetUserFromXml(xml);
            payload.Add("email", email);

            var notification = new OAuthAuthenticatedContext(Context, Options, Backchannel, tokens, payload)
            {
                Properties = properties,
                Principal = new ClaimsPrincipal(identity)
            };

            var identifier = VKAuthenticationHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var firstName = VKAuthenticationHelper.GetFirstName(payload);
            if (!string.IsNullOrEmpty(firstName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var lastName = VKAuthenticationHelper.GetLastName(payload);
            if (!string.IsNullOrEmpty(lastName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, lastName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var userName = VKAuthenticationHelper.GetUserName(payload);
            if (!string.IsNullOrEmpty(userName))
            {
                // Many VK accounts do not set the ScreenName field.
                identity.AddClaim(new Claim("urn:vk:screenname", userName, ClaimValueTypes.String, Options.ClaimsIssuer));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Events.Authenticated(notification);

            return new AuthenticationTicket(notification.Principal, notification.Properties, notification.Options.AuthenticationScheme);
        }

        private JObject GetUserFromXml(string xml)
        {
            var doc = JObject.Parse(xml)["response"][0].ToString();
            return JObject.Parse(doc);
        }
    }
}