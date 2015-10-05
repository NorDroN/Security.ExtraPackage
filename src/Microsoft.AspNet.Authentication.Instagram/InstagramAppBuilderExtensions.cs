// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Instagram;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="InstagramMiddleware"/>.
    /// </summary>
    public static class InstagramAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using Instagram.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseInstagramAuthentication(this IApplicationBuilder app, InstagramOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<InstagramMiddleware>(options);
        }

        /// <summary>
        /// Authenticate users using Instagram.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseInstagramAuthentication(this IApplicationBuilder app, Action<InstagramOptions> configureOptions)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            var options = new InstagramOptions();
            if (configureOptions != null)
            {
                configureOptions(options);
            }
            return app.UseInstagramAuthentication(options);
        }
    }
}
