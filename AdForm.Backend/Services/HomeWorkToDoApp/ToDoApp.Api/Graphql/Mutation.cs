using ToDoApp.Business;
using AutoMapper;
using HotChocolate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using AdForm.Core;
using ToDoApp.Shared;
using System.Net;

namespace ToDoApp.Api
{
    /// <summary>
    /// Mutation class for GraphQl.
    /// </summary>
    public class Mutation
    {
        private readonly ILableAppService _lableAppService;
        private readonly IToDoItemAppService _toDoItemAppService;
        private readonly IToDoListAppService _toDoListAppService;
       
        /// <summary>
        /// Create new instance of <see cref="Mutation"/> class.
        /// </summary>
        /// <param name="lableAppService"> Label Service</param>
        /// <param name="toDoItemAppService"> ToDoItem Service</param>
        /// <param name="toDoListAppService">List service</param>
        /// <param name="mapper">Auto mapper</param>
        public Mutation([Service]ILableAppService lableAppService, [Service]IToDoItemAppService toDoItemAppService, [Service]IToDoListAppService toDoListAppService)
        {
            _lableAppService = lableAppService;
            _toDoItemAppService = toDoItemAppService;
            _toDoListAppService = toDoListAppService;
        }

        #region Labels

        /// <summary>
        /// Add label
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<LabelResponseDto> AddLabel([Service] IHttpContextAccessor contextAccessor, LabelRequestDto request)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (null == request || string.IsNullOrWhiteSpace(request.Name))
                throw new ApiException(ErrorMessage.Label_Name_Empty, HttpStatusCode.BadRequest, ApiExceptionType.ValidationError);

            var createdLabel = await _lableAppService.CreateAsync(request);
            return createdLabel;
        }

        /// <summary>
        /// Delete Label
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [Authorize]
        public async Task<bool> DeleteLabel([Service] IHttpContextAccessor contextAccessor, long id)
        {
            await _lableAppService.DeleteAsync(id);
            return true;
        }

        #endregion

        #region TodoLists

        /// <summary>
        /// Add list
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ToDoListResponseDto> AddList([Service] IHttpContextAccessor contextAccessor, ToDoListRequestDto request)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (null == request || string.IsNullOrWhiteSpace(request.ListName))
                throw new ArgumentNullException();

            request.UserId = userId;

            var addedItem = await _toDoListAppService.CreateAsync(request);
            return addedItem;
        }
        /// <summary>
        /// Update todolist
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<bool> UpdateList([Service] IHttpContextAccessor contextAccessor, UpdateToDoListRequestDto request)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (null == request || string.IsNullOrWhiteSpace(request.ListName))
                throw new ArgumentNullException();

            ToDoListRequestDto toDoListRequestDto = new ToDoListRequestDto();
            toDoListRequestDto.ListName = request.ListName;
            toDoListRequestDto.UserId = userId;

            await _toDoListAppService.UpdateAsync(request.ListId, toDoListRequestDto);

            return true;
        }

        /// <summary>
        /// Delete todolist
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<bool> DeleteList([Service] IHttpContextAccessor contextAccessor, int id)
        {

            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (id <= 0)
                throw new ArgumentNullException();

            await _toDoListAppService.DeleteAsync(id, userId);

            return true; ;
        }
        #endregion

        #region TodoItems
        /// <summary>
        /// Add todoitem
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ToDoItemResponseDto> AddItem([Service] IHttpContextAccessor contextAccessor, ToDoItemRequestDto request)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (null == request || string.IsNullOrWhiteSpace(request.ItemName))
                throw new ArgumentNullException();

            request.UserId = userId;

            var addedItem = await _toDoItemAppService.CreateAsync(request);
            return addedItem;
        }

        /// <summary>
        /// Update todoItem
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<bool> UpdateItem([Service] IHttpContextAccessor contextAccessor, UpdateToDoItemRequestDto request)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (null == request || string.IsNullOrWhiteSpace(request.ItemName))
                throw new ArgumentNullException();

            ToDoItemRequestDto toDoItemRequestDto = new ToDoItemRequestDto();
            toDoItemRequestDto.ItemName = request.ItemName;
            toDoItemRequestDto.UserId = userId;
          
            await _toDoItemAppService.UpdateAsync(request.ItemId, toDoItemRequestDto);

            return true;
        }

        /// <summary>
        /// Delete todoitem
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<bool> Delete([Service] IHttpContextAccessor contextAccessor, int id)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            if (id <= 0)
                throw new ArgumentNullException();

            await _toDoItemAppService.DeleteAsync(id, userId);

            return true; ;
        }

        #endregion
    }
}
