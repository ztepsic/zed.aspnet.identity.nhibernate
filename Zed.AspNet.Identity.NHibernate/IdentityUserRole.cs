namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents an NHibernate user belonging to a role.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class IdentityUserRole<TKey> {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets the Role ID for the role.
        /// </summary>
        public virtual TKey RoleId { get; set; }

        /// <summary>
        /// Gets or sets the ID for the user that is in the role.
        /// </summary>
        public virtual TKey UserId { get; set; }

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods
        #endregion

    }
}
