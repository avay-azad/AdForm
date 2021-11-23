using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;
using ToDoApp.Api.Controllers.V1;
using ToDoApp.Business;

namespace AdForm.Backend.Test.ControllersTests
{
    public class LabelControllerTests : BaseController
    {
        private LabelController controller;
        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            controller = new LabelController(LabelService.Object)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            IActionResult result = await controller.Post(new LabelRequestDto { Name = "Labeltest" }, new CreateLabelRequestValidator { });
            CreatedAtActionResult response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }
    }
}
