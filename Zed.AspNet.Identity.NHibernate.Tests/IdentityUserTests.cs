using System.Collections;
using NHibernate.Linq.Clauses;
using NUnit.Framework;

namespace Zed.AspNet.Identity.NHibernate.Tests {
    [TestFixture]
    public class IdentityUserTests {

        [Test]
        public void Ctor_IdentityUser_Created() {
            // Arrange

            // Act
            var user = new IdentityUser("exampleUsername");

            // Assert
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Claims);
            Assert.IsNotNull(user.Roles);
            Assert.IsNotNull(user.Logins);
        }

        [Test]
        public void AddRole_Role_RoleAddedToUser() {
            // Arrange
            var user = new IdentityUser("exampleUsername");
            var role = new IdentityRole("ExampleRole");

            // Act
            var result = user.AddRole(role);

            // Assert
            Assert.IsTrue(result);
            Assert.Contains(role, user.Roles as ICollection);
        }

    }
}
