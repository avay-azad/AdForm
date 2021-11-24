using AdForm.Core;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ToDoApp.Business;

namespace ToDoApp.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILableAppService _lableAppService;

        public LabelController(ILableAppService lableAppService)
        {
            _lableAppService = lableAppService;
        }

        /// <summary>
        /// Get all label
        /// </summary>
        /// <returns>Returns Action Result type based on Success or Failure. </returns>
        /// <response code="200">Gets all labels records.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="500">Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<PagedList<LabelResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters request)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            PagedList<LabelResponseDto> labels = await _lableAppService.GetAllAsync(request, userId);
            var metadata = new
            {
                labels.TotalCount,
                labels.PageSize,
                labels.CurrentPage,
                labels.TotalPages,
                labels.HasNext,
                labels.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(new APIResponse<PagedList<LabelResponseDto>> { IsSucess = true, Result = labels });
        }

        /// <summary>
        /// Get specific label record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result" type based on Success/Failure.</returns>
        /// <response code="200"> Gets specified label.</response>
        /// <response code="404">A label with the specified label ID was not found.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="500">Some unexpected error occurred</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<PagedList<LabelResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabel([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var label = await _lableAppService.GetByIdAsync(id, userId);
            return Ok(new APIResponse<LabelResponseDto> { IsSucess = true, Result = label });
        }

        /// <summary>
        /// Create Label record.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="201">Creates Label and returns location where it is created.</response>
        /// <response code="400">Invalid or missing request parameter.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="500">Some unexpected error occurred.</response>
        /// <response code="409">Conflict label name</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(LabelResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LabelRequestDto request, [FromServices] IValidator<LabelRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            request.UserId = userId;
            var label = await _lableAppService.CreateAsync(request);

            return CreatedAtAction(nameof(GetLabel), new { id = label.LabelId }, label);

        }

        /// <summary>
        /// Delete specific label record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specified label record.</response>
        /// <response code="404"> A label with the specified label ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500">Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            await _lableAppService.DeleteAsync(id, userId);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }
    }
}