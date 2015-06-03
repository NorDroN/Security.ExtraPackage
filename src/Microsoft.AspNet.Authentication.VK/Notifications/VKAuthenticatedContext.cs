// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.Authentication.VK
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class VKAuthenticatedContext : OAuthAuthenticatedContext
    {
        /// <summary>
        /// Initializes a new <see cref="VKAuthenticatedContext"/>.
        /// </summary>
        /// <param name="context">The HTTP environment.</param>
        /// <param name="user">The JSON-serialized user.</param>
        /// <param name="tokens">The VK Access token.</param>
        public VKAuthenticatedContext(HttpContext context, OAuthAuthenticationOptions options, JObject user, TokenResponse tokens)
            : base(context, options, user, tokens)
        {
            Id = TryGetValue(user, "uid");
            ScreenName = TryGetValue(user, "screen_name");
            Email = TryGetValue(user, "email");
            FirstName = TryGetValue(user, "first_name");
            LastName = TryGetValue(user, "last_name");
            Name = FirstName + " " + LastName;
        }

        /// <summary>
        /// Gets the VK user ID.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Gets the VK screen name.
        /// </summary>
        public string ScreenName { get; private set; }

        /// <summary>
        /// Gets the VK email.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the user's first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the user's last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public string Name { get; private set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }
    }
}
