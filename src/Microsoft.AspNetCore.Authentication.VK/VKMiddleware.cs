// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Authentication.VK
{
    /// <summary>
    /// An ASP.NET Core middleware for authenticating users using VK.
    /// </summary>
    public class VKMiddleware : OAuthMiddleware<VKOptions>
    {
        /// <summary>
        /// Initializes a new <see cref="VKMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the HTTP pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="encoder"></param>
        /// <param name="sharedOptions"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        public VKMiddleware(
            RequestDelegate next,
            IDataProtectionProvider dataProtectionProvider,
            ILoggerFactory loggerFactory,
            UrlEncoder encoder,
            IOptions<SharedAuthenticationOptions> sharedOptions,
            IOptions<VKOptions> options)
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
                // TODO: Should we just add these by default when we create the Options?
                Options.Scope.Add("email");
            }
        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler{T}"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler{T}"/> configured with the <see cref="VKOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<VKOptions> CreateHandler()
        {
            return new VKHandler(Backchannel);
        }
    }
}