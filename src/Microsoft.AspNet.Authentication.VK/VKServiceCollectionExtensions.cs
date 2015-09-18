// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.VK;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Internal;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// Extension methods for using <see cref="VKMiddleware"/>.
    /// </summary>
    public static class VKServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureVKAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<VKOptions> configure)
        {
            return services.Configure(configure);
        }

        public static IServiceCollection ConfigureVKAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config)
        {
            return services.Configure<VKOptions>(config);
        }
    }
}