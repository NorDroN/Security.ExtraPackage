// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;
using Newtonsoft.Json.Linq;
using System;

namespace Microsoft.AspNet.Authentication.LinkedIn
{
    internal class LinkedInAuthenticationHandler : OAuthAuthenticationHandler<LinkedInAuthenticationOptions, ILinkedInAuthenticationNotifications>
    {
        public LinkedInAuthenticationHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> GetUserInformationAsync(AuthenticationProperties properties, TokenResponse tokens)
        {
            var graphAddress = Options.UserInformationEndpoint + "?format=json&oauth2_access_token=" + Uri.EscapeDataString(tokens.AccessToken);

            var graphResponse = await Backchannel.GetAsync(graphAddress, Context.RequestAborted);
            graphResponse.EnsureSuccessStatusCode();
            var text = await graphResponse.Content.ReadAsStringAsync();
            var user = JObject.Parse(text);

            var context = new LinkedInAuthenticatedContext(Context, Options, user, tokens);
            var identity = new ClaimsIdentity(
                Options.ClaimsIssuer,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            if (!string.IsNullOrEmpty(context.Id))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.UserName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName, ClaimValueTypes.String, Options.ClaimsIssuer));
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
            if (!string.IsNullOrEmpty(context.Url))
            {
                identity.AddClaim(new Claim("urn:linkedin:link", context.Url, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            context.Properties = properties;
            context.Principal = new ClaimsPrincipal(identity);

            await Options.Notifications.Authenticated(context);

            return new AuthenticationTicket(context.Principal, context.Properties, context.Options.AuthenticationScheme);
        }
    }
}