// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Newtonsoft.Json.Linq;
using System;

namespace Microsoft.AspNetCore.Authentication.LinkedIn
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from LinkedIn after a successful authentication process.
    /// </summary>
    public static class LinkedInHelper
    {
        /// <summary>
        /// Gets the LinkedIn user ID.
        /// </summary>
        public static string GetId(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("id");
        }

        /// <summary>
        /// Gets the user's first name.
        /// </summary>
        public static string GetFirstName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("firstName");
        }

        /// <summary>
        /// Gets the user's last name.
        /// </summary>
        public static string GetLastName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("lastName");
        }

        /// <summary>
        /// Gets the LinkedIn username.
        /// </summary>
        public static string GetUserName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("formattedName");
        }

        /// <summary>
        /// Gets the LinkedIn email.
        /// </summary>
        public static string GetEmail(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("emailAddress");
        }

        /// <summary>
        /// Gets the user's link.
        /// </summary>
        public static string GetLink(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("publicProfileUrl");
        }
    }
}
