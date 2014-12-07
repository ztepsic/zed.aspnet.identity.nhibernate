using System;
using Zed.Domain;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents entity type for a user's login (i.e. facebook, google).
    /// </summary>
    public class IdentityUserLogin : ValueObject {

        #region Fields and Properties

        /// <summary>
        /// Login provider for the login (i.e. facebook, google)
        /// </summary>
        private readonly string loginProvider;

        /// <summary>
        /// Gets login provider for the login  (i.e. facebook, google).
        /// </summary>
        public virtual string LoginProvider { get { return loginProvider; } }

        /// <summary>
        /// The key representing the login for the provider.
        /// </summary>
        private readonly string providerKey;

        /// <summary>
        /// Gets the key representing the login for the provider.
        /// </summary>
        public virtual string ProviderKey { get { return providerKey; } }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Private constructor for NHibernate support for lazy loading.
        /// </summary>
        private IdentityUserLogin() {
            loginProvider = null;
            providerKey = null;
        }

        /// <summary>
        /// Creates user login
        /// </summary>
        /// <param name="loginProvider">Loginprovider for the login (i.e. facebook, google)</param>
        /// <param name="providerKey">The key representing the login for the provider.</param>
        public IdentityUserLogin(string loginProvider, string providerKey) {
            if (string.IsNullOrWhiteSpace(loginProvider)) { throw new ArgumentNullException("loginProvider"); }
            this.loginProvider = loginProvider;

            if (string.IsNullOrWhiteSpace(providerKey)) { throw new ArgumentNullException("providerKey"); }
            this.providerKey = providerKey;
        }

        #endregion

    }

}
