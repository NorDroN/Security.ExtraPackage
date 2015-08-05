// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Internal;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.Authentication.Instagram
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from Instagram after a successful authentication process.
    /// </summary>
    public static class InstagramAuthenticationHelper
    {
        /// <summary>
        /// Gets the Instagram user ID.
        /// </summary>
        public static string GetId([NotNull] JObject user) => user.Value<string>("id");

        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public static string GetFullName([NotNull] JObject user) => user.Value<string>("full_name");

        /// <summary>
        /// Gets the Instagram username.
        /// </summary>
        public static string GetUserName([NotNull] JObject user) => user.Value<string>("username");
    }
}
