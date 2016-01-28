// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Authentication.VK
{
    public static class VKDefaults
    {
        public const string AuthenticationScheme = "VK";

        public const string AuthorizationEndpoint = "https://oauth.vk.com/authorize";

        public const string TokenEndpoint = "https://oauth.vk.com/access_token";

        public const string UserInformationEndpoint = "https://api.vk.com/method/users.get";
    }
}
