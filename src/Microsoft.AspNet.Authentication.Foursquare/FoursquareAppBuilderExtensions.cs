// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Foursquare;
using Microsoft.Framework.Internal;
using Microsoft.Framework.OptionsModel;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="FoursquareAuthenticationMiddleware"/>.
    /// </summary>
    public static class FoursquareAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using Foursquare.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseFoursquareAuthentication([NotNull] this IApplicationBuilder app, Action<FoursquareAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            return app.UseMiddleware<FoursquareAuthenticationMiddleware>(
                 new ConfigureOptions<FoursquareAuthenticationOptions>(configureOptions ?? (o => { }))
                 {
                     Name = optionsName
                 });
        }
    }
}
