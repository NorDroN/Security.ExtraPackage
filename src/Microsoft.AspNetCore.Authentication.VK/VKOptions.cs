// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.VK;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Configuration options for <see cref="VKMiddleware"/>.
    /// </summary>
    public class VKOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="VKOptions"/>.
        /// </summary>
        public VKOptions()
        {
            AuthenticationScheme = VKDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-VK");
            AuthorizationEndpoint = VKDefaults.AuthorizationEndpoint;
            TokenEndpoint = VKDefaults.TokenEndpoint;
            UserInformationEndpoint = VKDefaults.UserInformationEndpoint;
        }
    }
}
