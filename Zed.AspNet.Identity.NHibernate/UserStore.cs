using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Zed.NHibernate.AspNet.Identity;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents an NHibernate implementation of a user store that supports IUserStore, IUserLoginStore, IUserClaimStore and IUserRoleStore.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim.</typeparam>
    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : IUserLoginStore<TUser, TKey>, 
        IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, 
        IUserSecurityStampStore<TUser, TKey>, IQueryableUserStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, 
        IUserPhoneNumberStore<TUser, TKey>, IUserTwoFactorStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, 
        IUserStore<TUser, TKey>, IDisposable 
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TKey : IEquatable<TKey>
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserClaim : IdentityUserClaim<TKey>, new() {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets a value that indicates whether to call SaveChanges after Create/Update/Delete.
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Gets or sets whether to dispose the DbContext during Dispose.
        /// TODO: Entity framework context, try NHibernate
        /// </summary>
        public bool DisposeContext { get; set; }

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods

        #region IUserStore<TUser, TKey>

        /// <summary>
        /// Asynchronously inserts an entity.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task CreateAsync(TUser user) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously marks an entity for deletion.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task DeleteAsync(TUser user) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously updates an entity.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task UpdateAsync(TUser user) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously finds a user by ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<TUser> FindByIdAsync(TKey userId) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously finds a user by name.
        /// </summary>
        /// <param name="userName">The name of the user to find.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<TUser> FindByNameAsync(string userName)  { throw new NotImplementedException(); }

        #endregion

        #region IUserEmailStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the IsConfirmed property for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="confirmed">true to confirm the email; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously finds a user by e-mail.
        /// </summary>
        /// <param name="email">The e-mail of the user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<TUser> FindByEmailAsync(string email) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously sets the user e-mail.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="email">The e-mail of the user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetEmailAsync(TUser user, string email) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously gets the user's e-mail.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetEmailAsync(TUser user) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously returns whether the user email is confirmed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>true if the user email is confirmed; otherwise, false.</returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)  { throw new NotImplementedException(); }

        #endregion

        #region IUserPasswordStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the password hash for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously gets the password hash for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetPasswordHashAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously determines whether the user has a password set.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> HasPasswordAsync(TUser user) {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserLockoutStore<TUser, TKey>

        /// <summary>
        /// Asynchronously resets the account access failed count, typically after the account is successfully accessed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task ResetAccessFailedCountAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously returns the current number of failed access attempts. This number usually will be reset whenever the password is verified or the account is locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<int> GetAccessFailedCountAsync(TUser user) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously returns whether the user can be locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>true if the user can be locked out; otherwise, false.</returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user) { throw new NotImplementedException(); }

        #endregion

        #region IQueryableUserStore<TUser, TKey>

        /// <summary>
        /// Gets an IQueryable<T> of users.
        /// </summary>
        public IQueryable<TUser> Users {
            get { throw new NotImplementedException(); } 
        }

        /// <summary>
        /// Asynchronously adds a claim to a user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="claim"></param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task AddClaimAsync(TUser user, Claim claim) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously removes a claim from a user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="claim"></param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task RemoveClaimAsync(TUser user, Claim claim) {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserLoginStore<TUser, TKey>

        /// <summary>
        /// Asynchronously adds a login to the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login to add.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously removes a login from a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login to add.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the logins for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously returns the user associated with this login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<TUser> FindAsync(UserLoginInfo login)  { throw new NotImplementedException(); }

        #endregion

        #region IUserRoleStore<TUser, TKey>

        /// <summary>
        /// Asynchronously adds a user to a role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task AddToRoleAsync(TUser user, string roleName) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously removes a user from a role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task RemoveFromRoleAsync(TUser user, string roleName) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the names of the roles a user is a member of.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<IList<string>> GetRolesAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously determines whether the user is in the named role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> IsInRoleAsync(TUser user, string roleName) {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserClaimStore<TUser, TKey>

        /// <summary>
        /// Asynchronously returns the claims for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user) { throw new NotImplementedException(); }

        #endregion

        #region IUserTwoFactorStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the Two Factor provider for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="enabled">true to enable the Two Factor provider; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously determines whether the two-factor providers are enabled for the user. 
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user) {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserSecurityStampStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the security stamp for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="stamp">The security stamp.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetSecurityStampAsync(TUser user, string stamp) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously gets the security stamp for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetSecurityStampAsync(TUser user) {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserLockoutStore<TUser, TKey>

        /// <summary>
        /// Asynchronously returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered not locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously locks a user out until the specified end date (set to a past date, to unlock a user).
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="lockoutEnd">The end date of the lockout.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously records the failed attempt to access the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously sets whether the user can be locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="enabled">true to enable lockout; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled) { throw new NotImplementedException(); }

        #endregion

        #region IUserPhoneNumberStore<TUser, TKey>

        /// <summary>
        /// Asynchronously returns whether the user phone number is confirmed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously sets the PhoneNumberConfirmed property for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="confirmed">true to confirm the phone number; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously sets the user phone number.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously gets a user's phone number.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetPhoneNumberAsync(TUser user) {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Releases all resources used by the current instance of the UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.
        /// </summary>
        public void Dispose() { throw new NotImplementedException(); }

        /// <summary>
        /// Releases the unmanaged resources used by the UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> class
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) { throw new NotImplementedException(); }

        #endregion

    }
}
