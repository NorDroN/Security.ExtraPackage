// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.DataProtection;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

namespace Microsoft.AspNet.Authentication.LinkedIn
{
    /// <summary>
    /// An ASP.NET middleware for authenticating users using LinkedIn.
    /// </summary>
    public class LinkedInMiddleware : OAuthMiddleware<LinkedInOptions>
    {
        /// <summary>
        /// Initializes a new <see cref="LinkedInMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the HTTP pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="encoder"></param>
        /// <param name="sharedOptions"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        /// <param name="configureOptions"></param>
        public LinkedInMiddleware(
            RequestDelegate next,
            IDataProtectionProvider dataProtectionProvider,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            IOptions<SharedAuthenticationOptions> sharedOptions,
            LinkedInOptions options)
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
        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler"/> configured with the <see cref="LinkedInOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<LinkedInOptions> CreateHandler()
        {
            return new LinkedInHandler(Backchannel);
        }
    }
}