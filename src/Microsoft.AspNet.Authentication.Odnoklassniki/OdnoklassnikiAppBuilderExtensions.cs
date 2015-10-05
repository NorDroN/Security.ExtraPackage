// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Odnoklassniki;
using Microsoft.Framework.Internal;
using Microsoft.Extensions.OptionsModel;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="OdnoklassnikiMiddleware"/>.
    /// </summary>
    public static class OdnoklassnikiAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using Odnoklassniki.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseOdnoklassnikiAuthentication([NotNull] this IApplicationBuilder app, [NotNull] OdnoklassnikiOptions options)
        {
            return app.UseMiddleware<OdnoklassnikiMiddleware>(options);
        }

        /// <summary>
        /// Authenticate users using Odnoklassniki.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseOdnoklassnikiAuthentication([NotNull] this IApplicationBuilder app, Action<OdnoklassnikiOptions> configureOptions)
        {
            var options = new OdnoklassnikiOptions();
            if (configureOptions != null)
            {
                configureOptions(options);
            }
            return app.UseOdnoklassnikiAuthentication(options);
        }
    }
}
