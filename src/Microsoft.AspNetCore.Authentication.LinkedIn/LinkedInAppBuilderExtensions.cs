// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Authentication.LinkedIn;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods to add LinkedIn authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class LinkedInAppBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="LinkedInMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables LinkedIn authentication capabilities.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseLinkedInAuthentication(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<LinkedInMiddleware>();
        }

        /// <summary>
        /// Adds the <see cref="LinkedInMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables LinkedIn authentication capabilities.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="options">A <see cref="LinkedInOptions"/> that specifies options for the middleware.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseLinkedInAuthentication(this IApplicationBuilder app, LinkedInOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<LinkedInMiddleware>(Options.Create(options));
        }
    }
}
