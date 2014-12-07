using System;
using Zed.Domain;

namespace Zed.AspNet.Identity.NHibernate {
    /// <summary>
    /// Specifies an EntityType that represents one specific user claim.
    /// </summary>
    public class IdentityUserClaim : ValueObject {

        #region Fields and Properties

        /// <summary>
        /// Claim type
        /// </summary>
        private readonly string claimType;

        /// <summary>
        /// Gets the claim type.
        /// </summary>
        public virtual string ClaimType { get { return claimType; } }

        /// <summary>
        /// Claim value
        /// </summary>
        private readonly string claimValue;

        /// <summary>
        /// Gets the claim value
        /// </summary>
        public virtual string ClaimValue { get { return claimValue; } }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Private constructor for NHibernate support for lazy loading.
        /// </summary>
        private IdentityUserClaim() {
            claimType = null;
            claimValue = null;
        }

        /// <summary>
        /// Creates user claim
        /// </summary>
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        public IdentityUserClaim(string claimType, string claimValue) {
            if (string.IsNullOrWhiteSpace(claimType)) { throw new ArgumentNullException("claimType"); }
            this.claimType = claimType;

            if (string.IsNullOrWhiteSpace(claimValue)) { throw new ArgumentNullException("claimValue"); }
            this.claimValue = claimValue;
        }
         
        #endregion

        #region Methods
        #endregion

    }

}
