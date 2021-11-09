using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdForm.SDK;
using AdFormAssignment.Business;
using AdFormAssignment.Shared;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdFormAssignment.Api.Controllers.V1
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
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var labels = await _lableAppService.GetAsync();

            return Ok(new APIResponse<List<LabelResponseDto>> { IsSucess = true, Result = labels });
        }

        /// <summary>
        /// Get label by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabel([FromRoute] long id)
        {
            var label = await _lableAppService.GetAsync(id);
            return Ok(new APIResponse<LabelResponseDto> { IsSucess = true, Result = label });
        }

        /// <summary>
        /// Craete label
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LabelRequestDto request, [FromServices] IValidator<LabelRequestDto> validator)
        {
            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ApiException(ApiErrorMessage.Global_Request_Validation, HttpStatusCode.BadRequest,
                    ApiExceptionType.ValidationError, validationResult.Errors);

            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            var label = await _lableAppService.CreateAsync(request);

            return CreatedAtAction(nameof(GetLabel), new { label.Id }, request);

        }

        /// <summary>
        /// Delete label
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {

            await _lableAppService.DeleteAsync(id);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }

        /// <summary>
        /// Assign label to list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="toDoListAppService"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> AssignLabelToList([FromBody] AssignLabelRequestDto request, [FromRoute] long id
            , [FromServices] IToDoListAppService toDoListAppService)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            var label = _lableAppService.GetAsync(id);
            if (label == null)
                throw new ApiException(ErrorMessage.Label_Not_Exist_assign, HttpStatusCode.NotFound, ApiExceptionType.ItemAlreadyExists);

            request.LabelId = id;
            request.UserId = userId;

            bool isAssigned = await toDoListAppService.AssignLabel(request);
            return Ok(new APIResponse<object> { IsSucess = isAssigned, Result = null });
        }

        /// <summary>
        /// Assign label to item
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="toDoItemAppService"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> AssignLabelToItem([FromBody] AssignLabelRequestDto request, [FromRoute] long id
           , [FromServices] IToDoItemAppService toDoItemAppService)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);

            var label = _lableAppService.GetAsync(id);
            if (label == null)
                throw new ApiException(ErrorMessage.Label_Not_Exist_assign, HttpStatusCode.NotFound, ApiExceptionType.ItemAlreadyExists);

            request.LabelId = id;
            request.UserId = userId;

            bool isAssigned = await toDoItemAppService.AssignLabel(request);
            return Ok(new APIResponse<object> { IsSucess = isAssigned, Result = null });
        }
    }
}