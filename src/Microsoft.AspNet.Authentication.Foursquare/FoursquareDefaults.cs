// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Authentication.Foursquare
{
    public static class FoursquareDefaults
    {
        public const string AuthenticationScheme = "Foursquare";

        public const string AuthorizationEndpoint = "https://foursquare.com/oauth2/authenticate";

        public const string TokenEndpoint = "https://foursquare.com/oauth2/access_token";

        public const string UserInformationEndpoint = "https://api.foursquare.com/v2/users/self";
    }
}
