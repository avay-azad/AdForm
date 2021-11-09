using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AdFormAssignment.Business;
using AdFormAssignment.Shared;
using AdForm.SDK;
using FluentValidation.Results;
using System.Net;
using FluentValidation;

namespace AdFormAssignment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserAppService _userAppService;

        public UserController(ILogger<UserController> logger, IJwtUtils jwtUtils, IUserAppService userAppService)
        {
            _logger = logger;
            _jwtUtils = jwtUtils;
            _userAppService = userAppService;
        }

        [HttpPost(AppConstants.Authenticate)]
        public async Task<IActionResult> Authenticate(LoginRequestDto request, [FromServices] IValidator<LoginRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            long userId = await _userAppService.AuthenticateUser(request);

            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateToken(userId);
            return Ok(new APIResponse<string> { IsSucess = true, Result = token });
        }
    }

}