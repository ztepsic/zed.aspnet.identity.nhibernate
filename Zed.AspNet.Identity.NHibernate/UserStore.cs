using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Linq;
using Zed.NHibernate;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents an NHibernate implementation of a user store that supports IUserStore, IUserLoginStore, IUserClaimStore and IUserRoleStore.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim.</typeparam>
    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserClaim> : NHibernateRepository,
        IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, 
        IUserSecurityStampStore<TUser, TKey>, IQueryableUserStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, 
        IUserPhoneNumberStore<TUser, TKey>, IUserTwoFactorStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, 
        IUserStore<TUser, TKey>, IDisposable 
        where TUser : IdentityUser<TKey, TUserLogin, TRole, TUserClaim>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserLogin : IdentityUserLogin
        where TUserClaim : IdentityUserClaim
    {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets a value that indicates whether to call SaveChanges after Create/Update/Delete.
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        private bool isDisposed;

        /// <summary>
        /// Gets or sets a value that indicates whether to call dispose on the Session during Dispose.
        /// </summary>
        public bool DisposeSession { get; set; }

        #region IQueryableUserStore<TUser, TKey>

        /// <summary>
        /// Gets an <see cref="IQueryable{T}"/> of users.
        /// </summary>
        public IQueryable<TUser> Users { get { return Session.Query<TUser>(); } }

        #endregion

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Constructor that creates role store
        /// </summary>
        /// <param name="sessionFactory">NHibernate session factory</param>
        public UserStore(ISessionFactory sessionFactory) : base(sessionFactory) {
            AutoSaveChanges = true;
        }

        #endregion

        #region Methods

        #region IUserStore<TUser, TKey>

        /// <summary>
        /// Asynchronously inserts an entity.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task CreateAsync(TUser user) {
            throwIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            Session.SaveOrUpdate(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously marks an entity for deletion.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task DeleteAsync(TUser user) {
            throwIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            Session.Delete(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously updates an entity.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual async Task UpdateAsync(TUser user) { await CreateAsync(user); }

        /// <summary>
        /// Asynchronously finds a user by ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<TUser> FindByIdAsync(TKey userId) {
            throwIfDisposed();
            return Task.FromResult(Session.Get<TUser>(userId));
        }

        /// <summary>
        /// Asynchronously finds a user by name.
        /// </summary>
        /// <param name="userName">The name of the user to find.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<TUser> FindByNameAsync(string userName) {
            throwIfDisposed();
            if (string.IsNullOrWhiteSpace(userName)) { throw new ArgumentNullException("userName");}

            return Task.FromResult(
                Session.Query<TUser>()
                    .FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower()));
        }

        #endregion

        #region IUserEmailStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the IsConfirmed property for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="confirmed">true to confirm the email; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        /* 
         * TODO: whats the point of this method if it does not saves to database.
         * One can simply set EmailConfirmed on user instance and save that instance
         * to db. Maybe implementation of this method makes sense regards to interface contract.
         */
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed) {
            throwIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously returns whether the user email is confirmed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>true if the user email is confirmed; otherwise, false.</returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user) {
            throwIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Asynchronously sets the user e-mail.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="email">The e-mail of the user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetEmailAsync(TUser user, string email) {
            throwIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            user.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the user's e-mail.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetEmailAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.Email);
        }


        /// <summary>
        /// Asynchronously finds a user by e-mail.
        /// </summary>
        /// <param name="email">The e-mail of the user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<TUser> FindByEmailAsync(string email) {
            throwIfDisposed();
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException("email"); }

            return Task.FromResult(
                Session.Query<TUser>()
                    .FirstOrDefault(u => u.Email.ToLower() == email.ToLower()));
        }

        #endregion

        #region IUserPasswordStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the password hash for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the password hash for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetPasswordHashAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// Asynchronously determines whether the user has a password set.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> HasPasswordAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region IUserLockoutStore<TUser, TKey>

        /// <summary>
        /// Asynchronously resets the account access failed count, typically after the account is successfully accessed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task ResetAccessFailedCountAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously returns the current number of failed access attempts. This number usually will be reset whenever the password is verified or the account is locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<int> GetAccessFailedCountAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// Asynchronously sets whether the user can be locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="enabled">true to enable lockout; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.LockoutEnabled = enabled;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously returns whether the user can be locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>true if the user can be locked out; otherwise, false.</returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        /// Asynchronously returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered not locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.LockoutEndDateUtc.HasValue
                ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                : new DateTimeOffset());
        }

        /// <summary>
        /// Asynchronously locks a user out until the specified end date (set to a past date, to unlock a user).
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="lockoutEnd">The end date of the lockout.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.LockoutEndDateUtc = (lockoutEnd == DateTimeOffset.MinValue) 
                ? new DateTime?() 
                : lockoutEnd.UtcDateTime;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously records the failed attempt to access the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        #endregion

        #region IUserLoginStore<TUser, TKey>

        /// <summary>
        /// Asynchronously adds a login to the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login to add.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (login == null) { throw new ArgumentNullException(); }

            var userLogin = new IdentityUserLogin(login.LoginProvider, login.ProviderKey) as TUserLogin;

            user.AddLogin(userLogin);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously removes a login from a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login to add.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (login == null) { throw new ArgumentNullException(); }

            var userLogin = user.Logins.SingleOrDefault(ul => ul.LoginProvider == login.LoginProvider && ul.ProviderKey == login.ProviderKey);
            if (userLogin != null) {
                user.RemoveLogin(userLogin);
                await UpdateAsync(user);
            }
            
        }

        /// <summary>
        /// Asynchronously gets the logins for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            IList<UserLoginInfo> logins = user.Logins.Select(ul => new UserLoginInfo(ul.LoginProvider, ul.ProviderKey)).ToList();

            return Task.FromResult(logins);
        }

        /// <summary>
        /// Asynchronously returns the user associated with this login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<TUser> FindAsync(UserLoginInfo login) {
            throwIfDisposed();
            if (login == null) { throw new ArgumentNullException("login"); }

            var user = (from u in Session.Query<TUser>()
                       from l in u.Logins
                       where l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey
                       select u).SingleOrDefault();

            return Task.FromResult(user);
        }

        #endregion

        #region IUserRoleStore<TUser, TKey>

        /// <summary>
        /// Asynchronously adds a user to a role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task AddToRoleAsync(TUser user, string roleName) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (string.IsNullOrWhiteSpace(roleName)) {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }

            var identityRole = Session.Query<TRole>()
                .SingleOrDefault(r => r.Name.ToLower() == roleName.ToLower());

            if (identityRole == null) {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, roleName));
            }

            user.AddRole(identityRole);
            
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously removes a user from a role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public async Task RemoveFromRoleAsync(TUser user, string roleName) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (string.IsNullOrWhiteSpace(roleName)) { throw new ArgumentNullException("roleName"); }

            var identityRole = user.Roles.SingleOrDefault(r => r.Name.ToLower() == roleName.ToLower());

            if (identityRole == null) {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, roleName));
            }

            if (identityRole != null) {
                user.RemoveRole(identityRole);
                await UpdateAsync(user);
            }   

        }

        /// <summary>
        /// Asynchronously gets the names of the roles a user is a member of.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<IList<string>> GetRolesAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult((IList<string>) user.Roles.Select(r => r.Name));
        }

        /// <summary>
        /// Asynchronously determines whether the user is in the named role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> IsInRoleAsync(TUser user, string roleName) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (string.IsNullOrWhiteSpace(roleName)) { throw new ArgumentNullException("roleName", IdentityResources.ValueCannotBeNullOrEmpty); }

            var result = user.Roles.Any(r => r.Name.ToLower() == roleName);

            return Task.FromResult(result);
        }

        #endregion

        #region IUserClaimStore<TUser, TKey>

        /// <summary>
        /// Asynchronously returns the claims for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            IList<Claim> claims = (from identityUserClaim in user.Claims
                                   select new Claim(identityUserClaim.ClaimType, identityUserClaim.ClaimValue)).ToList();

            return Task.FromResult(claims);
        }


        /// <summary>
        /// Asynchronously adds a claim to a user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="claim"></param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task AddClaimAsync(TUser user, Claim claim) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (claim == null) { throw new ArgumentNullException("claim"); }

            var userClaim = new IdentityUserClaim(claim.Type, claim.Value) as TUserClaim;

            user.AddClaim(userClaim);                

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously removes a claim from a user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="claim"></param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public async Task RemoveClaimAsync(TUser user, Claim claim) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            if (claim == null) { throw new ArgumentNullException("claim"); }

            var identityUserClaim = user.Claims.SingleOrDefault(uc => uc.ClaimType == claim.Type && uc.ClaimValue == claim.Value);
            if (identityUserClaim != null) {
                user.RemoveClaim(identityUserClaim);
                await UpdateAsync(user);
            }
            
        }

        #endregion

        #region IUserTwoFactorStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the Two Factor provider for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="enabled">true to enable the Two Factor provider; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.TwoFactorEnabled = enabled;
            
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously determines whether the two-factor providers are enabled for the user. 
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion

        #region IUserSecurityStampStore<TUser, TKey>

        /// <summary>
        /// Asynchronously sets the security stamp for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="stamp">The security stamp.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetSecurityStampAsync(TUser user, string stamp) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the security stamp for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetSecurityStampAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion

        #region IUserPhoneNumberStore<TUser, TKey>

        /// <summary>
        /// Asynchronously returns whether the user phone number is confirmed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        /// Asynchronously sets the PhoneNumberConfirmed property for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="confirmed">true to confirm the phone number; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.PhoneNumberConfirmed = confirmed;
            
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously sets the user phone number.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            user.PhoneNumber = phoneNumber;

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets a user's phone number.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<string> GetPhoneNumberAsync(TUser user) {
            throwIfDisposed();
            if (user == null) { throw new ArgumentNullException("user"); }

            return Task.FromResult(user.PhoneNumber);
        }

        #endregion

        private void throwIfDisposed() {
            if (isDisposed) { throw new ObjectDisposedException(GetType().Name); }
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="UserStore{TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim}"/>.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="UserStore{TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim}"/> class
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (DisposeSession && disposing && Session != null) {
                Session.Dispose();
            }

            isDisposed = true;
        }

        #endregion

    }

    /// <summary>
    /// Represents an NHibernate implementation of a user store that supports IUserStore, IUserLoginStore, IUserClaimStore and IUserRoleStore,
    ///  where the identifier is the integer number.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    public class UserStore<TUser> : UserStore<TUser, IdentityRole, int, IdentityUserLogin, IdentityUserClaim>,
        IUserStore<TUser, int>, IDisposable
        where TUser : IdentityUser 
    {
        #region Constructors and Init

        /// <summary>
        /// Constructor that creates user store
        /// </summary>
        /// <param name="sessionFactory">NHibernate session factory</param>
        public UserStore(ISessionFactory sessionFactory) : base(sessionFactory) { }

        #endregion
    }
}
