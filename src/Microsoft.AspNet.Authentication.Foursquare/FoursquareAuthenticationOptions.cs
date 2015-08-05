// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.Foursquare
{
    /// <summary>
    /// Configuration options for <see cref="FoursquareAuthenticationMiddleware"/>.
    /// </summary>
    public class FoursquareAuthenticationOptions : OAuthAuthenticationOptions
    {
        /// <summary>
        /// Initializes a new <see cref="FoursquareAuthenticationOptions"/>.
        /// </summary>
        public FoursquareAuthenticationOptions()
        {
            AuthenticationScheme = FoursquareAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Foursquare");
            AuthorizationEndpoint = FoursquareAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = FoursquareAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = FoursquareAuthenticationDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = false;
        }
    }
}
