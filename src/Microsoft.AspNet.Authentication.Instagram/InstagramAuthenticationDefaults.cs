// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Authentication.Instagram
{
    public static class InstagramAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Instagram";

        public const string AuthorizationEndpoint = "https://api.instagram.com/oauth/authorize/";

        public const string TokenEndpoint = "https://api.instagram.com/oauth/access_token/";

        public const string UserInformationEndpoint = "https://api.instagram.com/v1/users/";
    }
}
