using AdForm.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public Mock<IJwtUtils> JwtUtils { get; }

        private readonly LabelResponseDto _labelResponseDto = new LabelResponseDto { LabelId = 1 };
        private readonly ToDoListResponseDto _toDoListResponseDto = new ToDoListResponseDto { Name = "Test", ToDoListId = 1 };
        private readonly ToDoItemResponseDto _toDoItemResponseDto = new ToDoItemResponseDto { Name = "Test", ToDoItemId = 1, ToDoListId = 1 };
        private long _userId = 1;

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
            JwtUtils = new Mock<IJwtUtils>();

            //Mock methods
            LabelService.Setup(p => p.CreateAsync(It.IsAny<LabelRequestDto>())).Returns(Task.FromResult(_labelResponseDto));
            LabelService.Setup(p => p.DeleteAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            LabelService.Setup(p => p.GetAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_labelResponseDto));
            UserService.Setup(p => p.AuthenticateUser(It.IsAny<LoginRequestDto>())).Returns(Task.FromResult(_userId));
            ToDoListService.Setup(p => p.CreateAsync(It.IsAny<ToDoListRequestDto>())).Returns(Task.FromResult(_toDoListResponseDto));
            ToDoListService.Setup(p => p.DeleteAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            ToDoListService.Setup(p => p.GetAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoListResponseDto));
            ToDoListService.Setup(p => p.UpdateAsync(It.IsAny<long>(), It.IsAny<UpdateToDoListRequestDto>())).Returns(Task.FromResult(_toDoListResponseDto));
            ToDoItemService.Setup(p => p.CreateAsync(It.IsAny<ToDoItemRequestDto>())).Returns(Task.FromResult(_toDoItemResponseDto));
            ToDoItemService.Setup(p => p.DeleteAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            ToDoItemService.Setup(p => p.GetAsync(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoItemResponseDto));
            ToDoItemService.Setup(p => p.UpdateAsync(It.IsAny<long>(), It.IsAny<UpdateToDoItemRequestDto>())).Returns(Task.FromResult(_toDoItemResponseDto));

        }

    }
}
