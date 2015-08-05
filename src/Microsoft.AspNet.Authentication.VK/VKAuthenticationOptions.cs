// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.VK
{
    /// <summary>
    /// Configuration options for <see cref="VKAuthenticationMiddleware"/>.
    /// </summary>
    public class VKAuthenticationOptions : OAuthAuthenticationOptions
    {
        /// <summary>
        /// Initializes a new <see cref="VKAuthenticationOptions"/>.
        /// </summary>
        public VKAuthenticationOptions()
        {
            AuthenticationScheme = VKAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-VK");
            AuthorizationEndpoint = VKAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = VKAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = VKAuthenticationDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = false;
        }
    }
}
