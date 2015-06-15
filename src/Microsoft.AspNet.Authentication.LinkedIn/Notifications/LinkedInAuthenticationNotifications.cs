// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.LinkedIn
{
    /// <summary>
    /// The default <see cref="ILinkedInAuthenticationNotifications"/> implementation.
    /// </summary>
    public class LinkedInAuthenticationNotifications : OAuthAuthenticationNotifications, ILinkedInAuthenticationNotifications
    {
        /// <summary>
        /// Initializes a new <see cref="LinkedInAuthenticationNotifications"/>.
        /// </summary>
        public LinkedInAuthenticationNotifications()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
        }

        /// <summary>
        /// Gets or sets the function that is invoked when the Authenticated method is invoked.
        /// </summary>
        public Func<LinkedInAuthenticatedContext, Task> OnAuthenticated { get; set; }

        /// <summary>
        /// Invoked whenever LinkedIn succesfully authenticates a user.
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task Authenticated(LinkedInAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }
    }
}
