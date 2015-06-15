// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Microsoft.AspNet.Authentication.Instagram
{
    internal class InstagramAuthenticationHandler : OAuthAuthenticationHandler<InstagramAuthenticationOptions, IInstagramAuthenticationNotifications>
    {
        public InstagramAuthenticationHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> GetUserInformationAsync(AuthenticationProperties properties, TokenResponse tokens)
        {
            var user = tokens.Response["user"] as JObject;

            var context = new InstagramAuthenticatedContext(Context, Options, user, tokens);
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
            if (!string.IsNullOrEmpty(context.FullName))
            {
                identity.AddClaim(new Claim("urn:instagram:fullname", context.FullName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            context.Properties = properties;
            context.Principal = new ClaimsPrincipal(identity);

            await Options.Notifications.Authenticated(context);

            return new AuthenticationTicket(context.Principal, context.Properties, context.Options.AuthenticationScheme);
        }
    }
}