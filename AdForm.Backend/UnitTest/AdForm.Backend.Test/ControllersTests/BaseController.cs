using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using ToDoApp.Business;

namespace AdForm.Backend.Test
{
    public class BaseController : MapperInitiator
    {
        public ControllerContext Context { get; }
        public ApiVersion Version { get; }
        public Mock<ILableAppService> LabelService { get; }
        public Mock<IToDoItemAppService> ToDoItemService { get; }
        public Mock<IToDoListAppService> ToDoListService { get; }
        public Mock<IUserAppService> UserService { get; }

      private readonly LabelResponseDto _labelResponseDto = new LabelResponseDto { LabelId =1};

        protected BaseController()
        {
            Context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            Context.HttpContext.Request.HttpContext.Items["UserId"] = "1";
            Version = new ApiVersion(1, 0);
            LabelService = new Mock<ILableAppService>();
            ToDoItemService = new Mock<IToDoItemAppService>();
            ToDoListService = new Mock<IToDoListAppService>();
            UserService = new Mock<IUserAppService>();


            //Mock methods
            LabelService.Setup(p => p.CreateAsync(It.IsAny<LabelRequestDto>())).Returns(Task.FromResult(_labelResponseDto));
            

        }

    }
}
