// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.VK;
using Microsoft.Framework.Internal;
using Microsoft.Framework.OptionsModel;

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
        public static IApplicationBuilder UseVKAuthentication([NotNull] this IApplicationBuilder app, [NotNull] VKOptions options)
        {
            return app.UseMiddleware<VKMiddleware>(options);
        }

        /// <summary>
        /// Authenticate users using VK.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseVKAuthentication([NotNull] this IApplicationBuilder app, Action<VKOptions> configureOptions)
        {
            var options = new VKOptions();
            if (configureOptions != null)
            {
                configureOptions(options);
            }
            return app.UseVKAuthentication(options);
        }
    }
}
