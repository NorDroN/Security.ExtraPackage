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

namespace Microsoft.AspNetCore.Authentication.VK
{
    internal class VKHandler : OAuthHandler<VKOptions>
    {
        public VKHandler(HttpClient httpClient)
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

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Options.AuthenticationScheme);
            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

            var identifier = VKHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var firstName = VKHelper.GetFirstName(payload);
            if (!string.IsNullOrEmpty(firstName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var lastName = VKHelper.GetLastName(payload);
            if (!string.IsNullOrEmpty(lastName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, lastName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var userName = VKHelper.GetUserName(payload);
            if (!string.IsNullOrEmpty(userName))
            {
                // Many VK accounts do not set the ScreenName field.
                identity.AddClaim(new Claim("urn:vk:screenname", userName, ClaimValueTypes.String, Options.ClaimsIssuer));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Events.CreatingTicket(context);

            return context.Ticket;
        }

        private JObject GetUserFromXml(string xml)
        {
            var doc = JObject.Parse(xml)["response"][0].ToString();
            return JObject.Parse(doc);
        }
    }
}