using AuthManagerAppLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthManagerAppTest
{
    [TestClass]
    public class UserDataAccessTests
    {
        [TestMethod]
        public void UserCreateDeleteTest()
        {
            var users = UserDataAccess.GetUsers();
            var newUserId = UserDataAccess.CreateUser("testUser", "user123");
            var updatedUsers = UserDataAccess.GetUsers();

            Assert.IsTrue(newUserId > 0);
            Assert.AreNotEqual(users, updatedUsers);

            UserDataAccess.DeleteUser(newUserId);
            Assert.AreEqual(users.Count, UserDataAccess.GetUsers().Count);
        }

        [TestMethod]
        public void CheckCreateUserTest()
        {
            const string username = "datest2";
            const string password = "testpassword";
            Assert.AreEqual(UserDataAccess.GetUserId(username, password), 0);

            var newUserId = UserDataAccess.CreateUser(username, password);
            Assert.AreNotEqual(newUserId, 0);

            var newUserId2 = UserDataAccess.CreateUser(username, password);
            Assert.AreEqual(newUserId, newUserId2);

            UserDataAccess.DeleteUser(newUserId);
        }

        [TestMethod]
        public void CanGetUserId()
        {
            const string username = "testUsers";
            const string password = "testPassword";
            Assert.AreEqual(UserDataAccess.GetUserId(username, password), 0);

            var userId = UserDataAccess.CreateUser(username, password);
            Assert.AreNotEqual(0, userId);
            Assert.AreEqual(userId, UserDataAccess.GetUserId(username, password));

            UserDataAccess.DeleteUser(userId);
        }
    }
}
