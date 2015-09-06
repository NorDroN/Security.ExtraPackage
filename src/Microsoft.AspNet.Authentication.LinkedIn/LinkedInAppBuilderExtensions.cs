// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.LinkedIn;
using Microsoft.Framework.Internal;
using Microsoft.Framework.OptionsModel;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Extension methods for using <see cref="LinkedInAuthenticationMiddleware"/>.
    /// </summary>
    public static class LinkedInAppBuilderExtensions
    {
        /// <summary>
        /// Authenticate users using LinkedIn.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to the configure method.</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseLinkedInAuthentication([NotNull] this IApplicationBuilder app, Action<LinkedInAuthenticationOptions> configureOptions = null, string optionsName = "")
        {
            return app.UseMiddleware<LinkedInAuthenticationMiddleware>(
                 new ConfigureOptions<LinkedInAuthenticationOptions>(configureOptions ?? (o => { })));
        }
    }
}
