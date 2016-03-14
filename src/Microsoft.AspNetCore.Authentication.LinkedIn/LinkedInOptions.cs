// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.LinkedIn;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Configuration options for <see cref="LinkedInMiddleware"/>.
    /// </summary>
    public class LinkedInOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="LinkedInOptions"/>.
        /// </summary>
        public LinkedInOptions()
        {
            AuthenticationScheme = LinkedInDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-LinkedIn");
            AuthorizationEndpoint = LinkedInDefaults.AuthorizationEndpoint;
            TokenEndpoint = LinkedInDefaults.TokenEndpoint;
            UserInformationEndpoint = LinkedInDefaults.UserInformationEndpoint;
        }
    }
}
