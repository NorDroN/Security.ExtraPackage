// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Instagram;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Configuration options for <see cref="InstagramMiddleware"/>.
    /// </summary>
    public class InstagramOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="InstagramOptions"/>.
        /// </summary>
        public InstagramOptions()
        {
            AuthenticationScheme = InstagramDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Instagram");
            AuthorizationEndpoint = InstagramDefaults.AuthorizationEndpoint;
            TokenEndpoint = InstagramDefaults.TokenEndpoint;
            UserInformationEndpoint = InstagramDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = false;
        }
    }
}
