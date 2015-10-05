// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.VK;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="VKMiddleware"/>.
    /// </summary>
    public static class VKAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using VK.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseVKAuthentication(this IApplicationBuilder app, VKOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<VKMiddleware>(options);
        }

        /// <summary>
        /// Authenticate users using VK.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseVKAuthentication(this IApplicationBuilder app, Action<VKOptions> configureOptions)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            var options = new VKOptions();
            if (configureOptions != null)
            {
                configureOptions(options);
            }
            return app.UseVKAuthentication(options);
        }
    }
}
