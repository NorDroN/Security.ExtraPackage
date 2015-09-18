// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Odnoklassniki;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Internal;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// Extension methods for using <see cref="OdnoklassnikiMiddleware"/>.
    /// </summary>
    public static class OdnoklassnikiServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureOdnoklassnikiAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<OdnoklassnikiOptions> configure)
        {
            return services.Configure(configure);
        }

        public static IServiceCollection ConfigureOdnoklassnikiAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config)
        {
            return services.Configure<OdnoklassnikiOptions>(config);
        }
    }
}