// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.LinkedIn
{
    /// <summary>
    /// Configuration options for <see cref="LinkedInAuthenticationMiddleware"/>.
    /// </summary>
    public class LinkedInAuthenticationOptions : OAuthAuthenticationOptions<ILinkedInAuthenticationNotifications>
    {
        /// <summary>
        /// Initializes a new <see cref="LinkedInAuthenticationOptions"/>.
        /// </summary>
        public LinkedInAuthenticationOptions()
        {
            AuthenticationScheme = LinkedInAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-LinkedIn");
            AuthorizationEndpoint = LinkedInAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = LinkedInAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = LinkedInAuthenticationDefaults.UserInformationEndpoint;
        }
    }
}
