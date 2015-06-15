// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Authentication.LinkedIn
{
    public static class LinkedInAuthenticationDefaults
    {
        public const string AuthenticationScheme = "LinkedIn";

        public const string AuthorizationEndpoint = "https://www.linkedin.com/uas/oauth2/authorization";

        public const string TokenEndpoint = "https://www.linkedin.com/uas/oauth2/accessToken";

        public const string UserInformationEndpoint = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,email-address,public-profile-url)";
    }
}
