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
using System.Linq;
using System;

namespace Microsoft.AspNetCore.Authentication.Odnoklassniki
{
    internal class OdnoklassnikiHandler : OAuthHandler<OdnoklassnikiOptions>
    {
        public OdnoklassnikiHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            // Signing.
            // Call API methods using access_token instead of session_key parameter
            // Calculate every request signature parameter sig using a little bit different way described in
            // http://dev.odnoklassniki.ru/wiki/display/ok/Authentication+and+Authorization
            // sig = md5( request_params_composed_string+ md5(access_token + application_secret_key)  )
            // Don't include access_token into request_params_composed_string
            var args = new Dictionary<string, string>();
            args.Add("application_key", Options.ClientPublicKey);
            args.Add("method", "users.getCurrentUser");
            args.Add("fields", "uid,name,first_name,last_name,email");
            var signature = string.Concat(args.OrderBy(x => x.Key).Select(x => string.Format("{0}={1}", x.Key, x.Value)).ToList());
            signature = GetMd5Hash(signature + GetMd5Hash(tokens.AccessToken + Options.ClientSecret));
            args.Add("access_token", tokens.AccessToken);
            args.Add("sig", signature);

            var endpoint = QueryHelpers.AddQueryString(Options.UserInformationEndpoint, args);

            var response = await Backchannel.GetAsync(endpoint, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Options.AuthenticationScheme);
            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, payload);

            var identifier = OdnoklassnikiHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var userName = OdnoklassnikiHelper.GetUserName(payload);
            if (!string.IsNullOrEmpty(userName))
            {
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var email = OdnoklassnikiHelper.GetEmail(payload);
            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var firstName = OdnoklassnikiHelper.GetFirstName(payload);
            if (!string.IsNullOrEmpty(firstName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            var lastName = OdnoklassnikiHelper.GetLastName(payload);
            if (!string.IsNullOrEmpty(lastName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, lastName, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Events.CreatingTicket(context);

            return context.Ticket;
        }

        private static string GetMd5Hash(string input)
        {
            var provider = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            bytes = provider.ComputeHash(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }
    }
}