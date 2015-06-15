// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.Instagram
{
    /// <summary>
    /// Configuration options for <see cref="InstagramAuthenticationMiddleware"/>.
    /// </summary>
    public class InstagramAuthenticationOptions : OAuthAuthenticationOptions<IInstagramAuthenticationNotifications>
    {
        /// <summary>
        /// Initializes a new <see cref="InstagramAuthenticationOptions"/>.
        /// </summary>
        public InstagramAuthenticationOptions()
        {
            AuthenticationScheme = InstagramAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Instagram");
            AuthorizationEndpoint = InstagramAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = InstagramAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = InstagramAuthenticationDefaults.UserInformationEndpoint;
        }
    }
}
