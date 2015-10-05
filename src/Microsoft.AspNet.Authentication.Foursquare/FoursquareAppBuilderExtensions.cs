// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Foursquare;
using Microsoft.Framework.Internal;
using Microsoft.Extensions.OptionsModel;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="FoursquareMiddleware"/>.
    /// </summary>
    public static class FoursquareAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using Foursquare.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <param name="options">The Middleware options.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseFoursquareAuthentication([NotNull] this IApplicationBuilder app, [NotNull] FoursquareOptions options)
        {
            return app.UseMiddleware<FoursquareMiddleware>(options);
        }

        /// <summary>
        /// Authenticate users using Foursquare.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseFoursquareAuthentication([NotNull] this IApplicationBuilder app, Action<FoursquareOptions> configureOptions)
        {
            var options = new FoursquareOptions();
            if (configureOptions != null)
            {
                configureOptions(options);
            }
            return app.UseFoursquareAuthentication(options);
        }
    }
}
