// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Framework.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.WebEncoders;

namespace Microsoft.AspNet.Authentication.Instagram
{
    /// <summary>
    /// An ASP.NET middleware for authenticating users using Instagram.
    /// </summary>
    public class InstagramMiddleware : OAuthMiddleware<InstagramOptions>
    {
        /// <summary>
        /// Initializes a new <see cref="InstagramMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the HTTP pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="encoder"></param>
        /// <param name="sharedOptions"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        /// <param name="configureOptions"></param>
        public InstagramMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] IDataProtectionProvider dataProtectionProvider,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IUrlEncoder encoder,
            [NotNull] IOptions<SharedAuthenticationOptions> sharedOptions,
            [NotNull] InstagramOptions options)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
        {
            if (Options.Scope.Count == 0)
            {
                // Instagram requires a scope string, so if the user didn't set one we go for the least possible.
                // TODO: Should we just add these by default when we create the Options?
                Options.Scope.Add("basic");
            }
        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler"/> configured with the <see cref="InstagramOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<InstagramOptions> CreateHandler()
        {
            return new InstagramHandler(Backchannel);
        }
    }
}