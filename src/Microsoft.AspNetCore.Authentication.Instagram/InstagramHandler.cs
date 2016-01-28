// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNetCore.Authentication.Instagram
{
    internal class InstagramHandler : OAuthHandler<InstagramOptions>
    {
        public InstagramHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var payload = tokens.Response["user"] as JObject;

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Options.AuthenticationScheme);
            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

            var identifier = InstagramHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var userName = InstagramHelper.GetUserName(payload);
            if (!string.IsNullOrEmpty(userName))
            {
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var fullName = InstagramHelper.GetFullName(payload);
            if (!string.IsNullOrEmpty(fullName))
            {
                identity.AddClaim(new Claim("urn:instagram:fullname", fullName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Events.CreatingTicket(context);

            return context.Ticket;
        }
    }
}