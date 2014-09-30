using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Zed.NHibernate.AspNet.Identity;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Represents a NHibernate implementation of a role store.
    /// </summary>
    /// <typeparam name="TRole">The type of the role.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role.</typeparam>
    public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>, IRoleStore<TRole, TKey>, IDisposable
        where TRole : IdentityRole<TKey, TUserRole>, new()
        where TUserRole : IdentityUserRole<TKey>, new() {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets a value that indicates whether to call dispose on the DbContext during Dispose.
        /// TODO
        /// </summary>
        public bool DisposeContext { get; set; }
        
        #endregion

        #region Constructors and Init
        #endregion

        #region Methods

        #region IQueryableRoleStore<TRole, TKey>

        /// <summary>
        /// Gets an IQueryable<T> of users.
        /// </summary>
        public IQueryable<TRole> Roles { get { throw new NotImplementedException(); } }

        #endregion

        #region IRoleStore<TRole, TKey>

        /// <summary>
        /// Asynchronously inserts an entity.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task CreateAsync(TRole role) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously marks an entity for deletion.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task DeleteAsync(TRole role) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously updates an entity.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public virtual Task UpdateAsync(TRole role) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously finds a role by name.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<TRole> FindByNameAsync(string roleName) { throw new NotImplementedException(); }

        /// <summary>
        /// Asynchronously finds a role by using the specified identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task<TRole> FindByIdAsync(TKey roleId) { throw new NotImplementedException(); }

        #endregion

        /// <summary>
        /// Releases all resources used by the current instance of the RoleStore<TRole, TKey, TUserRole>.
        /// </summary>
        public void Dispose() { throw new NotImplementedException(); }

        /// <summary>
        /// Releases the unmanaged resources used by the RoleStore<TRole, TKey, TUserRole> class and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">/// Releases the unmanaged resources used by the RoleStore<TRole, TKey, TUserRole> class and optionally releases the managed resources.</param>
        protected virtual void Dispose(bool disposing)  { throw new NotImplementedException(); }

        #endregion

    }
}
