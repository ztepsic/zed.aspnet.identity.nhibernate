using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Zed.Domain;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Default NHibernate IUser implementation
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TLogin">The type of the login.</typeparam>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    /// <typeparam name="TClaim">The type of the claim.</typeparam>
    /// </summary>
    public class IdentityUser<TKey, TLogin, TRole, TClaim> : Entity<TKey>, IUser<TKey>
        where TLogin : IdentityUserLogin<TKey>
        where TRole : IdentityUserRole<TKey>
        where TClaim : IdentityUserClaim<TKey>  {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets user email address
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets user password hash.
        /// The salted/hashed form of the user password.
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets an indicator if email is confirmed.
        /// True if the email is confirmed, default is false.
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets DateTime in UTC when lockout ends, any 
        // time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets an indicator if lockout is enabled.
        /// Is lockout enabled for this user.
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets access failed count.
        /// Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets user roles
        /// </summary>
        public virtual ICollection<TRole> Roles { get; private set; }

        /// <summary>
        /// Gets user logins.
        /// Navigation property for user logins
        /// </summary>
        public virtual ICollection<TLogin> Logins { get; private set; }

        /// <summary>
        /// Gets user claims.
        /// Navigation property for user claims
        /// </summary>
        public virtual ICollection<TClaim> Claims { get; private set; }

        /// <summary>
        /// Gets or sets user phone number.
        /// Navigation property for user roles
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets an indicator if phone number is confirmed.
        /// True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets an indicator if two factor is enabled.
        /// Is two factor enabled for the user.
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets security stamp.
        /// A random value that should change whenever a users 
        // credentials have changed (password changed, login removed).
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods
        #endregion

    }
}
