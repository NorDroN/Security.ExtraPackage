// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Internal;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.Authentication.LinkedIn
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from LinkedIn after a successful authentication process.
    /// </summary>
    public static class LinkedInAuthenticationHelper
    {
        /// <summary>
        /// Gets the LinkedIn user ID.
        /// </summary>
        public static string GetId([NotNull] JObject user) => user.Value<string>("id");

        /// <summary>
        /// Gets the user's first name.
        /// </summary>
        public static string GetFirstName([NotNull] JObject user) => user.Value<string>("firstName");

        /// <summary>
        /// Gets the user's last name.
        /// </summary>
        public static string GetLastName([NotNull] JObject user) => user.Value<string>("lastName");

        /// <summary>
        /// Gets the LinkedIn username.
        /// </summary>
        public static string GetUserName([NotNull] JObject user) => user.Value<string>("formattedName");

        /// <summary>
        /// Gets the LinkedIn email.
        /// </summary>
        public static string GetEmail([NotNull] JObject user) => user.Value<string>("emailAddress");

        /// <summary>
        /// Gets the user's link.
        /// </summary>
        public static string GetLink([NotNull] JObject user) => user.Value<string>("publicProfileUrl");
    }
}
