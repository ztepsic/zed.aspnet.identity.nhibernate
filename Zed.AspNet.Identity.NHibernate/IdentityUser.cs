using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        where TRole : IdentityRole<TKey>
        where TLogin : IdentityUserLogin
        where TClaim : IdentityUserClaim  {

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
        /// time in the past is considered not locked out.
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
        /// User roles
        /// </summary>
        private readonly ISet<TRole> roles;

        /// <summary>
        /// Gets user roles.
        /// Navigation property for user roles
        /// </summary>
        public virtual ICollection<TRole> Roles { get { return new ReadOnlyCollection<TRole>(roles.ToList()); } }

        /// <summary>
        /// User logins
        /// </summary>
        private readonly ISet<TLogin> logins;

        /// <summary>
        /// Gets user logins.
        /// Navigation property for user logins
        /// </summary>
        public virtual ICollection<TLogin> Logins { get { return new ReadOnlyCollection<TLogin>(logins.ToList()); } }


        /// <summary>
        /// User claims
        /// </summary>
        private readonly ISet<TClaim> claims;

        /// <summary>
        /// Gets user claims.
        /// Navigation property for user claims
        /// </summary>
        public virtual ICollection<TClaim> Claims { get { return new ReadOnlyCollection<TClaim>(claims.ToList()); } }

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
        /// credentials have changed (password changed, login removed).
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates identity user instance
        /// </summary>
        public IdentityUser() {
            roles = new HashSet<TRole>();
            logins = new HashSet<TLogin>();
            claims = new HashSet<TClaim>();
        }

        /// <summary>
        /// Constructor that takes a userName
        /// </summary>
        /// <param name="userName">UserName of the user</param>
        public IdentityUser(string userName) : this() {
            if (string.IsNullOrEmpty(userName)) { throw new ArgumentNullException("userName"); }

            UserName = userName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds role to the user
        /// </summary>
        /// <param name="role">Role to be added to the user</param>
        /// <returns>True if role is successfully added to the user, false otherwise.</returns>
        public virtual bool AddRole(TRole role) {
            if(role == null) { throw new ArgumentNullException("role"); }

            return roles.Add(role);
        }


        /// <summary>
        /// Removes role from the user
        /// </summary>
        /// <param name="role">Role to be removed from the user</param>
        /// <returns>True if role is successfully removed from the user, false otherwise.</returns>
        public virtual bool RemoveRole(TRole role) {
            if (role == null) { throw new ArgumentNullException("role"); }

            return roles.Remove(role);
        }

        /// <summary>
        /// Adds login to the user
        /// </summary>
        /// <param name="login">Login to be added to the user.</param>
        /// <returns>True if login is successfully added to the user, false otherwise.</returns>
        public virtual bool AddLogin(TLogin login) {
            if (login == null) { throw new ArgumentNullException("login"); }

            return logins.Add(login);
        }

        /// <summary>
        /// Removes login from the user
        /// </summary>
        /// <param name="login">Login to be removed from the user.</param>
        /// <returns>True if login is successfully removed from the user, false otherwise.</returns>
        public virtual bool RemoveLogin(TLogin login) {
            if (login == null) { throw new ArgumentNullException("login"); }

            return logins.Remove(login);
        }

        /// <summary>
        /// Adds claim to the user
        /// </summary>
        /// <param name="claim">Claim to be added to the user.</param>
        /// <returns>True if claim is successfully added to the user, false otherwise.</returns>
        public virtual bool AddClaim(TClaim claim) {
            if (claim == null) { throw new ArgumentNullException("claim"); }

            return claims.Add(claim);
        }

        /// <summary>
        /// Removes claim from the user
        /// </summary>
        /// <param name="claim">Claim to be removed from the user.</param>
        /// <returns>True if claim is successfully removed from the user, false otherwise.</returns>
        public virtual bool RemoveClaim(TClaim claim) {
            if (claim == null) { throw new ArgumentNullException("claim"); }

            return claims.Remove(claim);
        }


        #endregion

    }

    /// <summary>
    /// Default NHibernate IUser implementation,
    /// where the identifier is the integer number.
    /// </summary>
    public class IdentityUser : IdentityUser<int, IdentityUserLogin, IdentityRole, IdentityUserClaim> {

        /// <summary>
        /// Creates identity user instance
        /// </summary>
        protected IdentityUser() { }

        /// <summary>
        /// Constructor that takes a userName
        /// </summary>
        /// <param name="userName">UserName of the user</param>
        public IdentityUser(string userName) : base(userName) { }
    }

}
