// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Odnoklassniki;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Configuration options for <see cref="OdnoklassnikiMiddleware"/>.
    /// </summary>
    public class OdnoklassnikiOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new <see cref="OdnoklassnikiOptions"/>.
        /// </summary>
        public OdnoklassnikiOptions()
        {
            AuthenticationScheme = OdnoklassnikiDefaults.AuthenticationScheme;
            DisplayName = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Odnoklassniki");
            AuthorizationEndpoint = OdnoklassnikiDefaults.AuthorizationEndpoint;
            TokenEndpoint = OdnoklassnikiDefaults.TokenEndpoint;
            UserInformationEndpoint = OdnoklassnikiDefaults.UserInformationEndpoint;
        }

        //
        // Summary:
        //     Gets or sets the provider-assigned client public key.
        public string ClientPublicKey { get; set; }
    }
}
