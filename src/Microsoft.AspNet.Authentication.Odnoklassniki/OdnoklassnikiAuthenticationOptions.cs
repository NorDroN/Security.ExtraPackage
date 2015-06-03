// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.Odnoklassniki
{
    /// <summary>
    /// Configuration options for <see cref="OdnoklassnikiAuthenticationMiddleware"/>.
    /// </summary>
    public class OdnoklassnikiAuthenticationOptions : OAuthAuthenticationOptions<IOdnoklassnikiAuthenticationNotifications>
    {
        /// <summary>
        /// Initializes a new <see cref="OdnoklassnikiAuthenticationOptions"/>.
        /// </summary>
        public OdnoklassnikiAuthenticationOptions()
        {
            AuthenticationScheme = OdnoklassnikiAuthenticationDefaults.AuthenticationScheme;
            Caption = AuthenticationScheme;
            CallbackPath = new PathString("/signin-Odnoklassniki");
            AuthorizationEndpoint = OdnoklassnikiAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = OdnoklassnikiAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = OdnoklassnikiAuthenticationDefaults.UserInformationEndpoint;
        }

        //
        // Summary:
        //     Gets or sets the provider-assigned client public key.
        public string ClientPublicKey { get; set; }
    }
}
