// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.VK;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Internal;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// Extension methods for using <see cref="VKAuthenticationMiddleware"/>.
    /// </summary>
    public static class VKServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureVKAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<VKAuthenticationOptions> configure)
        {
            return services.ConfigureVKAuthentication(configure, optionsName: "");
        }

        public static IServiceCollection ConfigureVKAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<VKAuthenticationOptions> configure, string optionsName)
        {
            return services.Configure(configure, optionsName);
        }

        public static IServiceCollection ConfigureVKAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config)
        {
            return services.ConfigureVKAuthentication(config, optionsName: "");
        }

        public static IServiceCollection ConfigureVKAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config, string optionsName)
        {
            return services.Configure<VKAuthenticationOptions>(config, optionsName);
        }
    }
}