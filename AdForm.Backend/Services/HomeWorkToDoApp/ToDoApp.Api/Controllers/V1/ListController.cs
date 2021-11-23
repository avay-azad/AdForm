using AdForm.Core;
using ToDoApp.Business;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ToDoApp.Shared;
using Microsoft.AspNetCore.Http;

namespace ToDoApp.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IToDoListAppService _toDoListAppService;

        public ListController(IToDoListAppService toDoListAppService)
        {
            _toDoListAppService = toDoListAppService;
        }

        /// <summary>
        /// Get all todolist records
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns Action Result type based on Success or Failure. </returns>
        /// <response code="200"> Gets all todolist records.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters request)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            PagedList<ToDoListResponseDto> items = await _toDoListAppService.GetAsync(request, userId);
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
            return Ok(new APIResponse<PagedList<ToDoListResponseDto>> { IsSucess = true, Result = items });
        }

        /// <summary>
        /// Get specific todolist record.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Gets specific todolist record.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetList([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var toDolist = await _toDoListAppService.GetAsync(id, userId);

            return Ok(new APIResponse<ToDoListResponseDto> { IsSucess = true, Result = toDolist });
        }

        /// <summary>
        /// Create todolist record.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /List
        ///     {
        ///        "ListName": List1
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Creates todolist record and returns the location where created.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        /// <response code="409"> If List name is already exist for user.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<IActionResult> Post(ToDoListRequestDto request, [FromServices] IValidator<ToDoListRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            request.UserId = userId;

            var toDoList = await _toDoListAppService.CreateAsync(request);

            return CreatedAtAction(nameof(GetList), new { id = toDoList.ToDoListId }, toDoList);

        }

        /// <summary>
        /// Update specific todolist record.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="validator"></param>
        /// <returns>Returns ActionResult type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todolist record with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ToDoListRequestDto request, [FromRoute] long id, [FromServices] IValidator<ToDoListRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            request.UserId = userId;

            await _toDoListAppService.UpdateAsync(id, request);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Delete specific todolist record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specific todolist record.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoListAppService.DeleteAsync(id, userId);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Partial update specific todolist record with JsonPatch document.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Updates specific todolist record with details provided.</response>
        /// <response code="404"> A record with the specified todolist ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="400"> Any request parameter is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument request, [FromRoute] long id)
        {

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoListAppService.UpdateToDoListPatchAsync(id, userId, request);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Assign labels to specific todolist.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="lableAppService"></param>
        /// <param name="validator"></param>
        /// <returns>Ok if successful else not found.</returns>
        /// <response code="200"> Assigns specified label/s to todolist record.</response>
        /// <response code="404"> Error: 404 not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        /// <response code="500"> If any unhandled exception occured.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            bool isAssigned = await _toDoListAppService.AssignLabel(id, request, lableAppService);
            return Ok(new APIResponse<object> { IsSucess = isAssigned, Result = null });
        }
    }
}