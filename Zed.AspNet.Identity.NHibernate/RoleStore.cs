using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Linq;
using Zed.NHibernate;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents a NHibernate implementation of a role store.
    /// </summary>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class RoleStore<TRole, TKey> : NHibernateRepository,
        IQueryableRoleStore<TRole, TKey>, IRoleStore<TRole, TKey>, IDisposable
        where TRole : IdentityRole<TKey> {

        #region Fields and Properties

        private bool isDisposed;

        /// <summary>
        /// Gets or sets a value that indicates whether to call dispose on the Session during Dispose.
        /// </summary>
        public bool DisposeSession { get; set; }

        #region IQueryableRoleStore<TRole, TKey>

        /// <summary>
        /// Gets an <see cref="IQueryable{T}"/> of roles.
        /// </summary>
        public IQueryable<TRole> Roles { get { return Session.Query<TRole>(); } }

        #endregion
        
        #endregion

        #region Constructors and Init

        /// <summary>
        /// Constructor that creates role store
        /// </summary>
        /// <param name="sessionFactory">NHibernate session factory</param>
        public RoleStore(ISessionFactory sessionFactory) : base(sessionFactory) { }

        #endregion

        #region Methods

        #region IRoleStore<TRole, TKey>

        /// <summary>
        /// Asynchronously finds a role by using the specified identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<TRole> FindByIdAsync(TKey roleId) {
            throwIfDisposed();
            return Task.FromResult(Session.Get<TRole>(roleId));
        }

        /// <summary>
        /// Asynchronously finds a role by name.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<TRole> FindByNameAsync(string roleName) {
            throwIfDisposed();
            return Task.FromResult(
                Session.Query<TRole>()
                    .FirstOrDefault(u => u.Name.ToLower() == roleName.ToLower()));
        }

        /// <summary>
        /// Asynchronously inserts an role entity.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task CreateAsync(TRole role) {
            throwIfDisposed();
            if (role == null) {
                throw new ArgumentNullException("role");
            }
            Session.SaveOrUpdate(role);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously marks an entity for deletion.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task DeleteAsync(TRole role) {
            throwIfDisposed();
            if (role == null) {
                throw new ArgumentNullException("role");
            }

            Session.Delete(role);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously updates an entity.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual async Task UpdateAsync(TRole role) { await CreateAsync(role); }

        #endregion

        private void throwIfDisposed() {
            if (isDisposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="RoleStore{TRole, TKey}"/>.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="RoleStore{TRole, TKey}"/> class and optionally releases the managed resources.
        /// If disposing, calls dispose on the SessionFactory.
        /// </summary>
        /// <param name="disposing">If true calls dispose on the SessionFactory</param>
        protected virtual void Dispose(bool disposing) {
            if (DisposeSession && disposing && Session != null) {
                Session.Dispose();
            }

            isDisposed = true;
        }

        #endregion

    }

    /// <summary>
    /// Represents a NHibernate implementation of a role store where the identifier is the integer number.
    /// </summary>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    public class RoleStore<TRole> : RoleStore<TRole, int>
        where TRole : IdentityRole {

        #region Constructors and Init

        /// <summary>
        /// Constructor that creates role store
        /// </summary>
        /// <param name="sessionFactory">NHibernate session factory</param>
        public RoleStore(ISessionFactory sessionFactory) : base(sessionFactory) { }

        #endregion

    }
}
