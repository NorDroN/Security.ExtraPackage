// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Authentication.Odnoklassniki
{
    public static class OdnoklassnikiAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Odnoklassniki";

        public const string AuthorizationEndpoint = "http://www.odnoklassniki.ru/oauth/authorize";

        public const string TokenEndpoint = "http://api.odnoklassniki.ru/oauth/token.do";

        public const string UserInformationEndpoint = "http://api.odnoklassniki.ru/fb.do";
    }
}
