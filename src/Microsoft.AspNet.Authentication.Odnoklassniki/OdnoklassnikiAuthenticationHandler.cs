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

namespace Microsoft.AspNet.Authentication.Odnoklassniki
{
    internal class OdnoklassnikiAuthenticationHandler : OAuthAuthenticationHandler<OdnoklassnikiAuthenticationOptions, IOdnoklassnikiAuthenticationNotifications>
    {
        public OdnoklassnikiAuthenticationHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> GetUserInformationAsync(AuthenticationProperties properties, TokenResponse tokens)
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

            var graphAddress = Options.UserInformationEndpoint + "?" + string.Join("&", args.Select(x => string.Format("{0}={1}", x.Key, x.Value)));

            var graphResponse = await Backchannel.GetAsync(graphAddress, Context.RequestAborted);
            graphResponse.EnsureSuccessStatusCode();
            var text = await graphResponse.Content.ReadAsStringAsync();
            var user = JObject.Parse(text);

            var context = new OdnoklassnikiAuthenticatedContext(Context, Options, user, tokens);
            var identity = new ClaimsIdentity(
                Options.ClaimsIssuer,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            if (!string.IsNullOrEmpty(context.Id))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id, ClaimValueTypes.String, Options.ClaimsIssuer));
            }
            if (!string.IsNullOrEmpty(context.Name))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, context.Name, ClaimValueTypes.String, Options.ClaimsIssuer));
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
            context.Properties = properties;
            context.Principal = new ClaimsPrincipal(identity);

            await Options.Notifications.Authenticated(context);

            return new AuthenticationTicket(context.Principal, context.Properties, context.Options.AuthenticationScheme);
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