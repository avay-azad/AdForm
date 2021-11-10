using AdForm.SDK;
using AdFormAssignment.Business;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
    public class ListController : ControllerBase
    {
       private readonly IToDoListAppService _toDoListAppService;

        public ListController(IToDoListAppService toDoListAppService)
        {
            _toDoListAppService = toDoListAppService;
        }

        /// <summary>
        /// get list with paging and searching
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        /// get list by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetList([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var toDolist = await _toDoListAppService.GetAsync(id, userId);

            return Ok(new APIResponse<ToDoListResponseDto> { IsSucess = true, Result = toDolist });
        }

        /// <summary>
        /// Create List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
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

            return CreatedAtAction(nameof(GetList), new { toDoList.Id }, request);

        }

        /// <summary>
        /// Update list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
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
        /// Delete list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoListAppService.DeleteAsync(id, userId);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// update list partially
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument request, [FromRoute] long id)
        {

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            await _toDoListAppService.UpdateToDoListPatchAsync(id, userId, request);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }
    }
}