using System;
using Microsoft.AspNet.Identity;
using Zed.Domain;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents a role entity.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class IdentityRole<TKey> : Entity<TKey>, IRole<TKey> {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public virtual string Name { get; set; }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Protected constructor for NHibernate support for lazy loading.
        /// </summary>
        protected IdentityRole() { }

        /// <summary>
        /// Creates identity role instance
        /// </summary>
        /// <param name="roleName">Role name</param>
        public IdentityRole(string roleName) {
            if (string.IsNullOrEmpty(roleName)) { throw new ArgumentNullException("roleName"); }

            Name = roleName;
        }

        #endregion

    }

    /// <summary>
    /// Represents a role entity where the identifier is the integer number.
    /// </summary>
    public class IdentityRole : IdentityRole<int> {

        #region Constructors and Init

        /// <summary>
        /// Protected constructor for NHibernate support for lazy loading.
        /// </summary>
        protected IdentityRole() { }

        /// <summary>
        /// Creates identity role instance
        /// </summary>
        /// <param name="roleName">Role name</param>
        public IdentityRole(string roleName) : base(roleName) { }

        #endregion

    }

}
