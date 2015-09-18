// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.VK
{
    /// <summary>
    /// Configuration options for <see cref="VKMiddleware"/>.
    /// </summary>
    public class VKOptions : OAuthAuthenticationOptions
    {
        /// <summary>
        /// Initializes a new <see cref="VKOptions"/>.
        /// </summary>
        public VKOptions()
        {
            AuthenticationScheme = VKDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-VK");
            AuthorizationEndpoint = VKDefaults.AuthorizationEndpoint;
            TokenEndpoint = VKDefaults.TokenEndpoint;
            UserInformationEndpoint = VKDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = false;
        }
    }
}
