// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.WebEncoders;

namespace Microsoft.AspNet.Authentication.Foursquare
{
    /// <summary>
    /// An ASP.NET middleware for authenticating users using Foursquare.
    /// </summary>
    public class FoursquareMiddleware : OAuthMiddleware<FoursquareOptions>
    {
        /// <summary>
        /// Initializes a new <see cref="FoursquareMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the HTTP pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="encoder"></param>
        /// <param name="sharedOptions"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        /// <param name="configureOptions"></param>
        public FoursquareMiddleware(
            RequestDelegate next,
            IDataProtectionProvider dataProtectionProvider,
            ILoggerFactory loggerFactory,
            IUrlEncoder encoder,
            IOptions<SharedAuthenticationOptions> sharedOptions,
            FoursquareOptions options)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));

            if (dataProtectionProvider == null)
                throw new ArgumentNullException(nameof(dataProtectionProvider));

            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (encoder == null)
                throw new ArgumentNullException(nameof(encoder));

            if (sharedOptions == null)
                throw new ArgumentNullException(nameof(sharedOptions));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (Options.Scope.Count == 0)
            {
                // Foursquare requires a scope string, so if the user didn't set one we go for the least possible.
                // TODO: Should we just add these by default when we create the Options?
                Options.Scope.Add("basic");
            }
        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler"/> configured with the <see cref="FoursquareOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<FoursquareOptions> CreateHandler()
        {
            return new FoursquareHandler(Backchannel);
        }
    }
}