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

namespace Microsoft.AspNet.Authentication.Odnoklassniki
{
    /// <summary>
    /// An ASP.NET middleware for authenticating users using Odnoklassniki.
    /// </summary>
    public class OdnoklassnikiAuthenticationMiddleware : OAuthAuthenticationMiddleware<OdnoklassnikiAuthenticationOptions, IOdnoklassnikiAuthenticationNotifications>
    {
        /// <summary>
        /// Initializes a new <see cref="OdnoklassnikiAuthenticationMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the application pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        public OdnoklassnikiAuthenticationMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] IDataProtectionProvider dataProtectionProvider,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IUrlEncoder encoder,
            [NotNull] IOptions<ExternalAuthenticationOptions> externalOptions,
            [NotNull] IOptions<OdnoklassnikiAuthenticationOptions> options,
            ConfigureOptions<OdnoklassnikiAuthenticationOptions> configureOptions = null)
            : base(next, dataProtectionProvider, loggerFactory, encoder, externalOptions, options, configureOptions)
        {
            if (string.IsNullOrWhiteSpace(Options.ClientPublicKey))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_OptionMustBeProvided, nameof(Options.ClientPublicKey)));
            }

            if (Options.Notifications == null)
            {
                Options.Notifications = new OdnoklassnikiAuthenticationNotifications();
            }
        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler"/> configured with the <see cref="OdnoklassnikiAuthenticationOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<OdnoklassnikiAuthenticationOptions> CreateHandler()
        {
            return new OdnoklassnikiAuthenticationHandler(Backchannel);
        }
    }
}