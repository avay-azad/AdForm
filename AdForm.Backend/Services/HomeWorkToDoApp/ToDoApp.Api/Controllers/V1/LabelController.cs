using AdForm.Core;
using ToDoApp.Business;
using ToDoApp.Shared;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var labels = await _lableAppService.GetAsync(userId);

            return Ok(new APIResponse<List<LabelResponseDto>> { IsSucess = true, Result = labels });
        }

        /// <summary>
        /// Get specific label record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result" type based on Success/Failure.</returns>
        /// <response code="200"> Gets specified label.</response>
        /// <response code="404"> A label with the specified label ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabel([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            var label = await _lableAppService.GetAsync(id, userId);
            return Ok(new APIResponse<LabelResponseDto> { IsSucess = true, Result = label });
        }

        /// <summary>
        /// Create Label record.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validator"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="201"> Creates Label and returns location where it is created.</response>
        /// <response code="400"> Invalid request format.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
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

            return CreatedAtAction(nameof(GetLabel), new { id = label.LabelId }, request);

        }

        /// <summary>
        /// Delete specific label record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Action result type based on Success/Failure.</returns>
        /// <response code="200"> Deletes specified label record.</response>
        /// <response code="404"> A label with the specified label ID was not found.</response>
        /// <response code="401"> Authorization information is missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var userId = Convert.ToInt64(Request.HttpContext.Items["UserId"]);
            await _lableAppService.DeleteAsync(id, userId);

            return Ok(new APIResponse<object> { IsSucess = true, Result = null });
        }
    }
}