using System.Threading.Tasks;
using NUnit.Framework;

namespace Zed.AspNet.Identity.NHibernate.Tests {
    [TestFixture]
    public class RoleStoreTests : SQLiteNHibernateTestFixture {

        private RoleStore<IdentityRole> roleStore;

        [SetUp]
        public void SetUp() {
           roleStore = new RoleStore<IdentityRole>(SessionFactory);
        }

        private async Task<IdentityRole> createIdentityRole(string roleName) {
            var role = new IdentityRole(roleName);

            using (var trx = Session.BeginTransaction()) {
                await roleStore.CreateAsync(role);
                trx.Commit();
            }

            return role;

        }

        [Test]
        public async Task Create_Role_CreatedRoleInDb() {
            // Arrange

            // Act
            await createIdentityRole("RoleName");

            // Assert
        }

        [Test]
        public async Task FindById_Id_FetchedRoleFromDb() {
            // Arrange
            var role = await createIdentityRole("RoleName");

            // Act
            IdentityRole roleResult = null;
            using (Session.BeginTransaction()) {
                 roleResult = await roleStore.FindByIdAsync(role.Id);
            }

            // Assert
            Assert.IsNotNull(roleResult);
            Assert.AreEqual(role, roleResult);
        }

        [Test]
        public async Task FindByName_RoleName_FetchedRoleFromDb() {
            // Assert
            var role = await createIdentityRole("RoleName");

            // Act
            IdentityRole roleResult = null;
            using (Session.BeginTransaction()) {
                roleResult = await roleStore.FindByNameAsync(role.Name);
            }

            // Assert
            Assert.IsNotNull(roleResult);
            Assert.AreEqual(role, roleResult);
        }

        [Test]
        public async Task Delete_Role_DeletedRoleFromDb() {
            // Arrange
            var role = await createIdentityRole("RoleName");

            // Act
            IdentityRole roleResult1 = null;
            using (Session.BeginTransaction()) {
                roleResult1 = await roleStore.FindByIdAsync(role.Id);
            }

            IdentityRole roleResult2 = null;
            using (var trx = Session.BeginTransaction()) {
                await roleStore.DeleteAsync(role);
                roleResult2 = await roleStore.FindByIdAsync(role.Id);
                trx.Commit();
            }

            // Assert
            Assert.IsNotNull(roleResult1);
            Assert.AreEqual(role, roleResult1);
            Assert.IsNull(roleResult2);

        }

        [Test]
        public async Task Update_Role_UpdatedRoleInDb() {
            // Arrange
            var role = await createIdentityRole("RoleName");

            role.Name = "ChangedRoleName";

            // Act
            using (var trx = Session.BeginTransaction()) {
                await roleStore.UpdateAsync(role);
                trx.Commit();
            }

            IdentityRole roleResult = null;
            using (Session.BeginTransaction()) {
                roleResult = await roleStore.FindByIdAsync(role.Id);
            }

            // Assert
            Assert.IsNotNull(roleResult);
            Assert.AreEqual(role, roleResult);
            Assert.AreEqual("ChangedRoleName", roleResult.Name);
        }

    }
}
