// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Instagram;
using Microsoft.Framework.Internal;
using Microsoft.Framework.OptionsModel;

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
        public static IApplicationBuilder UseInstagramAuthentication([NotNull] this IApplicationBuilder app, Action<InstagramOptions> configureOptions = null, string optionsName = "")
        {
            return app.UseMiddleware<InstagramMiddleware>(
                 new ConfigureOptions<InstagramOptions>(configureOptions ?? (o => { })));
        }
    }
}
