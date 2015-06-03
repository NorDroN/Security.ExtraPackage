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

namespace Microsoft.AspNet.Authentication.VK
{
    /// <summary>
    /// An ASP.NET middleware for authenticating users using VK.
    /// </summary>
    public class VKAuthenticationMiddleware : OAuthAuthenticationMiddleware<VKAuthenticationOptions, IVKAuthenticationNotifications>
    {
        /// <summary>
        /// Initializes a new <see cref="VKAuthenticationMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the application pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        public VKAuthenticationMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] IDataProtectionProvider dataProtectionProvider,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IUrlEncoder encoder,
            [NotNull] IOptions<ExternalAuthenticationOptions> externalOptions,
            [NotNull] IOptions<VKAuthenticationOptions> options,
            ConfigureOptions<VKAuthenticationOptions> configureOptions = null)
            : base(next, dataProtectionProvider, loggerFactory, encoder, externalOptions, options, configureOptions)
        {
            if (Options.Notifications == null)
            {
                Options.Notifications = new VKAuthenticationNotifications();
            }
            if (Options.Scope.Count == 0)
            {
                // TODO: Should we just add these by default when we create the Options?
                Options.Scope.Add("email");
            }
        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler"/> configured with the <see cref="VKAuthenticationOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<VKAuthenticationOptions> CreateHandler()
        {
            return new VKAuthenticationHandler(Backchannel);
        }
    }
}