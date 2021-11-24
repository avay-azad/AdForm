using AdForm.Core;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ToDoApp.Business;

namespace ToDoApp.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IToDoItemAppService _toDoItemAppService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="toDoItemAppService"></param>
        public ItemController(IToDoItemAppService toDoItemAppService)
        {
            _toDoItemAppService = toDoItemAppService;
        }

        /// <summary>
        ///  Get all todoitem records.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns Action Result type based on Success or Failure. </returns>
        /// <response code="200"> Gets all todoitem records.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<PagedList<ToDoItemResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters request)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            PagedList<ToDoItemResponseDto> items = await _toDoItemAppService.GetAllAsync(request, userId);
            var metadata = new
            {
                items.TotalCount,
                items.PageSize,
                items.CurrentPage,
                items.TotalPages,
                items.HasNext,
                items.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(new APIResponse<PagedList<ToDoItemResponseDto>> { IsSucess = true, Result = items });
        }

        /// <summary>
        ///Get specific todoitem record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Gets specific todoitem record.</response>
        /// <response code="404"> Error: 404 not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<ToDoItemResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetItem([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var toDoitem = await _toDoItemAppService.GetByIdAsync(id, userId);

            return Ok(new APIResponse<ToDoItemResponseDto> { IsSucess = true, Result = toDoitem });
        }

        /// <summary>
        /// Create todoitem record.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="201"> Creates todoitem reecord and returns location where it is created.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="409"> ToDoItem is already exist for user.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ToDoItemResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Post(ToDoItemRequestDto request, [FromServices] IValidator<ToDoItemRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            request.UserId = userId;

            var toDoItem = await _toDoItemAppService.CreateAsync(request);

            return CreatedAtAction(nameof(GetItem), new { id = toDoItem.ToDoItemId }, toDoItem);

        }

        /// <summary>
        ///  Update specific todoitem record. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="validator"></param>
        /// <returns>Returns ActionResult type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todoitem reecord with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Put([FromBody] UpdateToDoItemRequestDto request, [FromRoute] long id, [FromServices] IValidator<UpdateToDoItemRequestDto> validator)
        {
            request.ToDoItemId = id;
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            request.UserId = userId;

            await _toDoItemAppService.UpdateAsync(id, request);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Partial update todoitem record with JsonPatch document.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todoitem record with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument request, [FromRoute] long id)
        {

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoItemAppService.UpdateToDoItemPatchAsync(id, userId, request);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Delete specific todoItem record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specific todoitem reecord.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoItemAppService.DeleteAsync(id, userId);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Assign labels to specific todoitem.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="lableAppService"></param>
        /// <param name="validator"></param>
        /// <returns>Ok if successful else not found.</returns>
        /// <response code="200"> Assigns specified label/s to todoitem record.</response>
        /// <response code="404"> Error: 404 not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> Some unexpected error occurred.</response>
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(APIResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}/AssignLabels")]
        public async Task<IActionResult> AssignLabels([FromBody] AssignLabelRequestDto request, [FromRoute] long id
            , [FromServices] ILableAppService lableAppService, [FromServices] IValidator<AssignLabelRequestDto> validator)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            request.UserId = userId;

            bool isAssigned = await _toDoItemAppService.AssignLabel(id, request, lableAppService);
            return Ok(new APIResponse<object> { IsSucess = isAssigned, Result = null });
        }
    }
}