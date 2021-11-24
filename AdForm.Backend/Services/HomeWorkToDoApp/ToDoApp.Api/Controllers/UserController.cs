using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ToDoApp.Business;
using ToDoApp.Shared;
using AdForm.Core;
using FluentValidation.Results;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserAppService _userAppService;

        public UserController(IJwtUtils jwtUtils, IUserAppService userAppService)
        {
            _jwtUtils = jwtUtils;
            _userAppService = userAppService;
        }

        /// <summary>
        /// Takes UserName and Password and generates token on successful authentication.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns>ApiResponse on User Login </returns>
        /// <response code="200"> Get jwt token.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(APIResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpPost(AppConstants.Authenticate)]
        public async Task<IActionResult> Authenticate(LoginRequestDto request, [FromServices] IValidator<LoginRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            long userId = await _userAppService.AuthenticateUser(request);

            var token = _jwtUtils.GenerateToken(userId);
            return Ok(new APIResponse<string> { IsSucess = true, Result = token });
        }
    }

}