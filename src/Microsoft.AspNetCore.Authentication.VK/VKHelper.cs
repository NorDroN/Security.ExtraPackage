// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Newtonsoft.Json.Linq;
using System;

namespace Microsoft.AspNetCore.Authentication.VK
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from VK after a successful authentication process.
    /// </summary>
    public static class VKHelper
    {
        /// <summary>
        /// Gets the VK user ID.
        /// </summary>
        public static string GetId(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("uid");
        }

        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public static string GetFirstName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("first_name");
        }

        /// <summary>
        /// Gets the user's link.
        /// </summary>
        public static string GetLastName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("last_name");
        }

        /// <summary>
        /// Gets the VK username.
        /// </summary>
        public static string GetUserName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("screen_name");
        }

        /// <summary>
        /// Gets the VK email.
        /// </summary>
        public static string GetEmail(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.Value<string>("email");
        }
    }
}
