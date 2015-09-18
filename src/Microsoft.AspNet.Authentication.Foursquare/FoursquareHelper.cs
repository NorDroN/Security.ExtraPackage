// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Internal;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.Authentication.Foursquare
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from Foursquare after a successful authentication process.
    /// </summary>
    public static class FoursquareHelper
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
        /// Gets the Foursquare username.
        /// </summary>
        public static string GetUserName([NotNull] JObject user) => user.Value<string>("firstName");

        /// <summary>
        /// Gets the Foursquare email.
        /// </summary>
        public static string GetEmail([NotNull] JObject user) => user["contact"].Value<string>("email");

        /// <summary>
        /// Gets the user's link.
        /// </summary>
        public static string GetLink([NotNull] JObject user) => user.Value<string>("url");
    }
}
