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
        private LabelController _labelController;
     
        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _labelController = new LabelController(LabelService.Object)
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
            IActionResult result = await _labelController.Post(new LabelRequestDto { Name = "Labeltest" }, new CreateLabelRequestValidator { });
            CreatedAtActionResult response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }
        /// <summary>
        /// Delete label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            IActionResult result = await _labelController.Delete(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelByIdTest()
        {
            IActionResult result = await _labelController.GetLabel(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
