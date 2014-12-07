using System.Threading.Tasks;
using NUnit.Framework;

namespace Zed.AspNet.Identity.NHibernate.Tests {
    [TestFixture]
    public class UserStoreTests : SQLiteNHibernateTestFixture {

        private UserStore<IdentityUser,
                IdentityRole,
                int,
                IdentityUserLogin,
                IdentityUserClaim> userStore;

        [SetUp]
        public void SetUp() {
            userStore = new UserStore<
                IdentityUser,
                IdentityRole,
                int,
                IdentityUserLogin,
                IdentityUserClaim>(SessionFactory);
        }

        private async Task<IdentityUser> createIdentityUser(string userName) {
            var user = new IdentityUser(userName);

            // Act
            using (var trx = Session.BeginTransaction()) {
                await userStore.CreateAsync(user);
                trx.Commit();
            }

            return user;

        }

        #region IUserStore<TUser, TKey>

        [Test]
        public async Task Create_IdentityUser_CreatedUserInDb() {
            // Arrange

            // Act
            await createIdentityUser("userName");

            // Assert
        }

        [Test]
        public async Task FindById_Id_FetchedUserFromDb() {
            // Arrange
            var user = await createIdentityUser("userName");

            // Act
            IdentityUser userResult = null;
            using (Session.BeginTransaction()) {
                userResult = await userStore.FindByIdAsync(user.Id);
            }

            // Assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(user, userResult);
        }

        [Test]
        public async Task FindByName_UserName_FetchedUserFromDb() {
            // Assert
            var user = await createIdentityUser("userName");

            // Act
            IdentityUser userResult = null;
            using (Session.BeginTransaction()) {
                userResult = await userStore.FindByNameAsync(user.UserName);
            }

            // Assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(user, userResult);
        }

        [Test]
        public async Task Delete_User_DeletedUserFromDb() {
            // Arrange
            var user = await createIdentityUser("userName");

            // Act
            IdentityUser userResult1 = null;
            using (Session.BeginTransaction()) {
                userResult1 = await userStore.FindByIdAsync(user.Id);
            }

            IdentityUser userResult2 = null;
            using (var trx = Session.BeginTransaction()) {
                await userStore.DeleteAsync(user);
                userResult2 = await userStore.FindByIdAsync(user.Id);
                trx.Commit();
            }

            // Assert
            Assert.IsNotNull(userResult1);
            Assert.AreEqual(user, userResult1);
            Assert.IsNull(userResult2);

        }

        [Test]
        public async Task Update_Role_UpdatedRoleInDb() {
            // Arrange
            var user = await createIdentityUser("userName");

            user.UserName = "ChangedUserName";

            // Act
            using (var trx = Session.BeginTransaction()) {
                await userStore.UpdateAsync(user);
                trx.Commit();
            }

            IdentityUser userResult = null;
            using (Session.BeginTransaction()) {
                userResult = await userStore.FindByIdAsync(user.Id);
            }

            // Assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(user, userResult);
            Assert.AreEqual("ChangedUserName", userResult.UserName);
        }

        #endregion

        #region IUserEmailStore<TUser, TKey>

        [Test]
        public async Task SetEmailConfirmedAsync_ToUser_SetEmailConfirmedForUser() {
            // Arrange
            var user = await createIdentityUser("userName");
            const bool isEmailConfirmed = true;

            // Act
            using (var trx = Session.BeginTransaction()) {
                await userStore.SetEmailConfirmedAsync(user, isEmailConfirmed);
                trx.Commit();
            }

            IdentityUser userResult = null;
            using (Session.BeginTransaction()) {
                userResult = await userStore.FindByIdAsync(user.Id);
            }

            // Assert
            Assert.IsNotNull(userResult);
            Assert.IsTrue(userResult.EmailConfirmed);
        }

        [Test]
        public async Task GetEmailConfirmedAsync_UserWithEmailConfirmed_True() {
            // Arrange
            var user = await createIdentityUser("userName");

            using (var trx = Session.BeginTransaction()) {
                await userStore.SetEmailConfirmedAsync(user, true);
                trx.Commit();
            }

            // Act
            var isConfirmed = false;
            using (var trx = Session.BeginTransaction()) {
                isConfirmed = await userStore.GetEmailConfirmedAsync(user);
            }
            

            // Asert
            Assert.IsTrue(isConfirmed);
        }

        [Test]
        public async Task SetEmailAsync_UserToSetEmail_UserWithSetEmail() {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public async Task GetEmailAsync_UserWithSetEmail_UserEmail() {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public async Task FindByEmailAsync_ExistingEmail_FetchdUserWithRequestedEmail() {
            // Arrange
            var user = await createIdentityUser("userName");
            user.Email = "emailExample";

            // Act
            IdentityUser userResult = null;
            using (Session.BeginTransaction()) {
                userResult = await userStore.FindByEmailAsync(user.Email);
            }

            // Assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(user.Email, userResult.Email);
            Assert.AreEqual(user, userResult);
        }

        #endregion

        #region IUserPasswordStore<TUser, TKey>

        [Test]
        public async Task SetPasswordHashAsync() {
            
        }

        [Test]
        public async Task GetPasswordHashAsync() {
            
        }

        [Test]
        public async Task HasPasswordAsync() {
            
        }

        #endregion

        #region IUserLockoutStore<TUser, TKey>

        [Test]
        public async Task ResetAccessFailedCountAsync() {
            
        }

        [Test]
        public async Task GetAccessFailedCountAsync() {
            
        }

        [Test]
        public async Task GetLockoutEnabledAsync() {
            
        }

        [Test]
        public async Task GetLockoutEndDateAsync() {

        }

        [Test]
        public async Task SetLockoutEndDateAsync() {

        }

        [Test]
        public async Task IncrementAccessFailedCountAsync() {

        }

        [Test]
        public async Task SetLockoutEnabledAsync() {

        }

        #endregion

        #region IUserSecurityStampStore<TUser, TKey>

        [Test]
        public async Task SetSecurityStampAsync() {
            
        }

        [Test]
        public async Task GetSecurityStampAsync() {
            
        }

        #endregion

        #region IUserLoginStore<TUser, TKey>

        [Test]
        public async Task AddLoginAsync() {
            
        }

        [Test]
        public async Task RemoveLoginAsync() {
            
        }

        [Test]
        public async Task GetLoginsAsync() {
            
        }

        [Test]
        public async Task FindAsync() {
            
        }

        #endregion

        #region IUserRoleStore<TUser, TKey>

        [Test]
        public async Task AddToRoleAsync() {
            
        }

        [Test]
        public async Task RemoveFromRoleAsync() {
            
        }

        [Test]
        public async Task GetRolesAsync() {
            
        }

        [Test]
        public async Task IsInRoleAsync() {
            
        }

        #endregion

        #region IUserClaimStore<TUser, TKey>

        [Test]
        public async Task GetClaimsAsync() {
            
        }

        [Test]
        public async Task AddClaimAsync() {
            
        }

        [Test]
        public async Task RemoveClaimAsync() {
            
        }

        #endregion

        #region IUserTwoFactorStore<TUser, TKey>

        [Test]
        public async Task SetTwoFactorEnabledAsync() {
            
        }

        [Test]
        public async Task GetTwoFactorEnabledAsync() {
            
        }

        #endregion

        #region IUserPhoneNumberStore<TUser, TKey>

        [Test]
        public async Task GetPhoneNumberConfirmedAsync() {
            
        }

        [Test]
        public async Task SetPhoneNumberConfirmedAsync() {
            
        }

        [Test]
        public async Task SetPhoneNumberAsync() {
            
        }

        [Test]
        public async Task GetPhoneNumberAsync() {
            
        }

        #endregion


    }
}
