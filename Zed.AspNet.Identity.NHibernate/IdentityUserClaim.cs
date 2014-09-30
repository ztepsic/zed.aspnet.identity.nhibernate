namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Specifies an EntityType that represents one specific user claim.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityUserClaim<TKey> {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets the claim type.
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public virtual int Id { get; set; }

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
