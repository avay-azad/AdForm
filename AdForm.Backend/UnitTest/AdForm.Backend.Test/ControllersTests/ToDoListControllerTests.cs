using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Api.Controllers.V1;
using ToDoApp.Business;

namespace AdForm.Backend.Test.ControllersTests
{
    /// <summary>
    /// Lists controller tests.
    /// </summary>
    public class ToDoListControllerTests : BaseController
    {
        private ListController _listController;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _listController = new ListController(ToDoListService.Object)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddListTest()
        {
            IActionResult result = await _listController.Post(new ToDoListRequestDto { ListName = "test" }, new CreateListRequestValidator { });
            CreatedAtActionResult response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Update list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateListTest()
        {
            IActionResult result = await _listController.Put(new ToDoListRequestDto { ListName = "test"}, 1, new CreateListRequestValidator { });
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteListTest()
        {
            IActionResult result = await _listController.Delete(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get list test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetListByIdTest()
        {
            IActionResult result = await _listController.GetList(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
