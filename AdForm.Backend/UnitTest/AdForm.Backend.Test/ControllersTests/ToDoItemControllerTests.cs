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
    public class ToDoItemControllerTests : BaseController
    {
        private ItemController _itemController;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _itemController = new ItemController(ToDoItemService.Object)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddItemTest()
        {
            IActionResult result = await _itemController.Post(new ToDoItemRequestDto { ItemName = "test" , ToDoListId =1}, new CreateItemRequestValidator { });
            CreatedAtActionResult response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Update item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateItemTest()
        {
            IActionResult result = await _itemController.Put(new UpdateToDoItemRequestDto { ItemName = "test" }, 1, new UpdateItemRequestValidator { });
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteItemTest()
        {
            IActionResult result = await _itemController.Delete(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetItemByIdTest()
        {
            IActionResult result = await _itemController.GetItem(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
