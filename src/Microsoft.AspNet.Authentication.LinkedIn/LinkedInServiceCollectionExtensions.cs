// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.LinkedIn;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Internal;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// Extension methods for using <see cref="LinkedInAuthenticationMiddleware"/>.
    /// </summary>
    public static class LinkedInServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureLinkedInAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<LinkedInAuthenticationOptions> configure)
        {
            return services.ConfigureLinkedInAuthentication(configure, optionsName: "");
        }

        public static IServiceCollection ConfigureLinkedInAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<LinkedInAuthenticationOptions> configure, string optionsName)
        {
            return services.Configure(configure, optionsName);
        }

        public static IServiceCollection ConfigureLinkedInAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config)
        {
            return services.ConfigureLinkedInAuthentication(config, optionsName: "");
        }

        public static IServiceCollection ConfigureLinkedInAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config, string optionsName)
        {
            return services.Configure<LinkedInAuthenticationOptions>(config, optionsName);
        }
    }
}