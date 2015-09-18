// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Framework.Internal;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.WebEncoders;

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
            [NotNull] RequestDelegate next,
            [NotNull] IDataProtectionProvider dataProtectionProvider,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IUrlEncoder encoder,
            [NotNull] IOptions<SharedAuthenticationOptions> sharedOptions,
            [NotNull] IOptions<LinkedInOptions> options,
            ConfigureOptions<LinkedInOptions> configureOptions = null)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options, configureOptions)
        {

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