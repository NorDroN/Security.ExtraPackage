// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Foursquare;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Configuration options for <see cref="FoursquareMiddleware"/>.
    /// </summary>
    public class FoursquareOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="FoursquareOptions"/>.
        /// </summary>
        public FoursquareOptions()
        {
            AuthenticationScheme = FoursquareDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Foursquare");
            AuthorizationEndpoint = FoursquareDefaults.AuthorizationEndpoint;
            TokenEndpoint = FoursquareDefaults.TokenEndpoint;
            UserInformationEndpoint = FoursquareDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = false;
        }
    }
}
