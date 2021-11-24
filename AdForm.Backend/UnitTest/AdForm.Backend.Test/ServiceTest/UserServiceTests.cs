using AdForm.Core;
using AdForm.DBService;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDoApp.Business;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.ServiceTest
{
    /// <summary>
    /// User service tests.
    /// </summary>
    public class UserServiceTests : MapperInitiator
    {
        private Mock<IUserDataService> _userDataService;
        private IUserAppService _userAppService;
        private readonly Users _validUser = new Users { UserName = "avay", Password = AesSymmetric.AesEncrypt("Avay@123", "Psv0k64fyVGGxZfdgBNyQrwqB/0sNgq1X5YlnjePweY="), UserId = 1 };
        private readonly Users _noValidUser = new Users { UserName = "avay", Password = AesSymmetric.AesEncrypt("Avay123", "Psv0k64fyVGGxZfdgBNyQrwqB/0sNgq1X5YlnjePweY="), UserId = 0 };

        /// <summary>
        /// Set up.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            IOptions<AppSettings> appsetting = Options.Create<AppSettings>(new AppSettings());
            appsetting.Value.AesSymmetricKey = "Psv0k64fyVGGxZfdgBNyQrwqB/0sNgq1X5YlnjePweY=";
            appsetting.Value.Secret = "AdFormJwtSecret@!@#$%";
            _userDataService = new Mock<IUserDataService>();
            _userAppService = new UserAppService(_userDataService.Object, Mapper, appsetting);
        }

        /// <summary>
        /// Auth valid test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authenticate_ValidUserNameAndPassword()
        {
            _userDataService.Setup(p => p.GetUsers(It.IsAny<Users>())).Returns(Task.FromResult(_validUser));
            long userId = await _userAppService.AuthenticateUser(new LoginRequestDto() { UserName = "Avay", Password = "Avay@123" });
            Assert.IsTrue(userId == 1);
            Assert.AreEqual(1, userId);
        }

        /// <summary>
        /// Auth invalid test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authenticate_InvalidUserNameAndPassword()
        {
            try
            {
                _userDataService.Setup(p => p.GetUsers(It.IsAny<Users>())).Returns(Task.FromResult(_noValidUser));
                await _userAppService.AuthenticateUser(new LoginRequestDto() { UserName = "ajsdkjsa", Password = "kjsadksa" });
                Assert.Fail();
            }
            catch (AuthenticationException )
            {
                Assert.IsTrue(true);
            }
        }
    }
}
