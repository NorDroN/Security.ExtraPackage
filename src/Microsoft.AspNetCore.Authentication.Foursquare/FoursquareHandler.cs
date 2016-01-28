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
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Authentication.Foursquare
{
    internal class FoursquareHandler : OAuthHandler<FoursquareOptions>
    {
        public FoursquareHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, new Dictionary<string, string>
            {
                { "oauth_token", tokens.AccessToken },
                { "v", "20131201" }
            });

            var response = await Backchannel.GetAsync(endpoint, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync())["response"]["user"] as JObject;

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Options.AuthenticationScheme);
            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

            var identifier = FoursquareHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var userName = FoursquareHelper.GetUserName(payload);
            if (!string.IsNullOrEmpty(userName))
            {
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var email = FoursquareHelper.GetEmail(payload);
            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var firstName = FoursquareHelper.GetFirstName(payload);
            if (!string.IsNullOrEmpty(firstName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var lastName = FoursquareHelper.GetLastName(payload);
            if (!string.IsNullOrEmpty(lastName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, lastName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var link = FoursquareHelper.GetLink(payload);
            if (!string.IsNullOrEmpty(link))
            {
                identity.AddClaim(new Claim("urn:foursquare:link", link, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Events.CreatingTicket(context);

            return context.Ticket;
        }
    }
}