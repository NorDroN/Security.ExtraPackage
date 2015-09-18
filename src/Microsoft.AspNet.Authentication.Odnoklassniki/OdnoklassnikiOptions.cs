// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.Odnoklassniki
{
    /// <summary>
    /// Configuration options for <see cref="OdnoklassnikiMiddleware"/>.
    /// </summary>
    public class OdnoklassnikiOptions : OAuthAuthenticationOptions
    {
        /// <summary>
        /// Initializes a new <see cref="OdnoklassnikiOptions"/>.
        /// </summary>
        public OdnoklassnikiOptions()
        {
            AuthenticationScheme = OdnoklassnikiDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Odnoklassniki");
            AuthorizationEndpoint = OdnoklassnikiDefaults.AuthorizationEndpoint;
            TokenEndpoint = OdnoklassnikiDefaults.TokenEndpoint;
            UserInformationEndpoint = OdnoklassnikiDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = false;
        }

        //
        // Summary:
        //     Gets or sets the provider-assigned client public key.
        public string ClientPublicKey { get; set; }
    }
}
