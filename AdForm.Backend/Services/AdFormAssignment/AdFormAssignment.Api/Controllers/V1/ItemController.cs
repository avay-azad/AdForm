using AdForm.SDK;
using AdFormAssignment.Business;
using AdFormAssignment.Shared;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AdFormAssignment.Api.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IToDoItemAppService _toDoItemAppService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="toDoItemAppService"></param>
        public ItemController(ILogger<ItemController> logger, IToDoItemAppService toDoItemAppService)
        {
            _logger = logger;
            _toDoItemAppService = toDoItemAppService;
        }

        /// <summary>
        /// Get All Item with paging
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters request)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            PagedList<ToDoItemResponseDto> items = await _toDoItemAppService.GetAsync(request, userId);
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
        /// Get item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var toDoitem = await _toDoItemAppService.GetAsync(id, userId);

            return Ok(new APIResponse<ToDoItemResponseDto> { IsSucess = true, Result = toDoitem });
        }

        /// <summary>
        /// Create Item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(ToDoItemRequestDto request, [FromServices] IValidator<ToDoItemRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            request.UserId = userId;

            var toDoItem = await _toDoItemAppService.CreateAsync(request);

            return CreatedAtAction(nameof(GetItem), new { toDoItem.Id }, request);

        }

        /// <summary>
        /// Update item 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ToDoItemRequestDto request, [FromRoute] long id, [FromServices] IValidator<ToDoItemRequestDto> validator)
        {
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
        /// Partially update item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument request, [FromRoute] long id)
        {

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoItemAppService.UpdateToDoItemPatchAsync(id, userId, request);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoItemAppService.DeleteAsync(id, userId);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }
    }
}