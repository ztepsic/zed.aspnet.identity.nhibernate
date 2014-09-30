using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Zed.AspNet.Identity.NHibernate;
using Zed.Domain;

namespace Zed.NHibernate.AspNet.Identity {
    /// <summary>
    /// Represents a role entity.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role.</typeparam>
    public class IdentityRole<TKey, TUserRole> : Entity<TKey>, IRole<TKey> 
        where TUserRole : IdentityUserRole<TKey> {

        #region Fields and Properties

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of users in the role.
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; private set; }

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods
        #endregion
    }
}
