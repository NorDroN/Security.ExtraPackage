// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.OAuth;

namespace Microsoft.AspNet.Authentication.Odnoklassniki
{
    /// <summary>
    /// Specifies callback methods which the <see cref="OdnoklassnikiAuthenticationMiddleware"></see> invokes to enable developer control over the authentication process.
    /// </summary>
    public interface IOdnoklassnikiAuthenticationNotifications : IOAuthAuthenticationNotifications
    {
        /// <summary>
        /// Invoked when Odnoklassniki succesfully authenticates a user.
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        Task Authenticated(OdnoklassnikiAuthenticatedContext context);
    }
}
