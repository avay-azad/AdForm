using AdForm.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Api.Controllers;
using ToDoApp.Business;
using ToDoApp.DataService;

namespace AdForm.Backend.Test.ControllersTests
{
    public class UserControllerTests : BaseController
    {
        private UserController _userController;
        private IOptions<AppSettings> _options;
        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _options = Options.Create(new AppSettings { Secret = "AdFormJwtSecret@!@#$%", AesSymmetricKey = "Psv0k64fyVGGxZfdgBNyQrwqB/0sNgq1X5YlnjePweY=" });
            _userController = new UserController(JwtUtils.Object, UserService.Object)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Authentication test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AuthenticateUserTest()
        {
            IActionResult result = await _userController.Authenticate(new LoginRequestDto { UserName = "avay", Password = "Avay@123" }, new LoginRequestValidator { });
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }

}
