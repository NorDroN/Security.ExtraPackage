// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.LinkedIn;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="LinkedInMiddleware"/>.
    /// </summary>
    public static class LinkedInAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using LinkedIn.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseLinkedInAuthentication(this IApplicationBuilder app, LinkedInOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<LinkedInMiddleware>(options);
        }

        /// <summary>
        /// Authenticate users using LinkedIn.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseLinkedInAuthentication(this IApplicationBuilder app, Action<LinkedInOptions> configureOptions)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            var options = new LinkedInOptions();
            if (configureOptions != null)
            {
                configureOptions(options);
            }
            return app.UseLinkedInAuthentication(options);
        }
    }
}
