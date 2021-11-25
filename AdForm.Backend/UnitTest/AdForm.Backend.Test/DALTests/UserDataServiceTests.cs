using AdForm.Core;
using AdForm.DBService;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.DALTests
{
    public class UserDataServiceTests : ToDoAppDbContextInitiator
    {
        private readonly IUserDataService _userDataService;
        public UserDataServiceTests()
        {
            _userDataService = new UserDataService(DBContext);
            DBContext.Users.Add(new Users
            {
                FirstName = "Aradhaya",
                LastName = "Azad",
                Password = AesSymmetric.AesEncrypt("Manager@1", "Psv0k64fyVGGxZfdgBNyQrwqB/0sNgq1X5YlnjePweY="),
                UserName = "aradhaya"
            });
            DBContext.SaveChanges();
        }

        /// Test for get register user.
        /// </summary>
        [Test]
        public async Task GetUserTest()
        {
            Users user = await _userDataService.GetUsers(new Users {UserName= "aradhaya" });

            Assert.NotNull(user);
        }

        /// Test for get not found user.
        /// </summary>
        [Test]
        public async Task GetUserNotFoundTest()
        {
            Users user = await _userDataService.GetUsers(new Users { UserName = "avay" });

            Assert.IsNull(user);
        }
    }
}
