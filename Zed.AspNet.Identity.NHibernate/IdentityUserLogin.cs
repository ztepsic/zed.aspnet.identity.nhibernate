namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents entity type for a user's login.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class IdentityUserLogin<TKey> {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets the login provider for the login.
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the key representing the login for the provider.
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the user ID for the user who owns this login.
        /// </summary>
        public virtual TKey UserId { get; set; }

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods
        #endregion

    }
}
