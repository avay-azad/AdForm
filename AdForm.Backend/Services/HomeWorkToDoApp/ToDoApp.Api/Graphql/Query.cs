using ToDoApp.Business;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Api
{
    public class Query
    {
        private readonly ILableAppService _lableAppService;
        private readonly IToDoItemAppService _toDoItemAppService;
        private readonly IToDoListAppService _toDoListAppService;

        /// <summary>
        /// Create new instance of <see cref="Query"/> class
        /// </summary>
        /// <param name="lableAppService"> Label Service</param>
        /// <param name="toDoItemAppService"> ToDoItem Service</param>
        /// <param name="toDoListAppService">List service</param>
        public Query([Service]ILableAppService lableAppService, [Service]IToDoItemAppService toDoItemAppService, [Service]IToDoListAppService toDoListAppService)
        {
            _lableAppService = lableAppService;
            _toDoItemAppService = toDoItemAppService;
            _toDoListAppService = toDoListAppService;
        }

        #region Labels

        /// <summary>
        /// Get labels.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <returns>Returns labels.</returns>
        [Authorize]
        [UsePaging(SchemaType = typeof(LabelType))]
        [UseFiltering]
        public async Task<IQueryable<LabelResponseDto>> Labels([Service] IHttpContextAccessor contextAccessor)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);
            var labels = (await _lableAppService.GetAsync(userId)).AsQueryable();
            return labels;
        }

        #endregion

        #region TodoItems

        /// <summary>
        /// Get todoitems.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <returns>Returns todoitems.</returns>
        [Authorize]
        [UsePaging(SchemaType = typeof(ItemsType))]
        [UseFiltering]
        public async Task<IQueryable<ToDoItemResponseDto>> Items([Service] IHttpContextAccessor contextAccessor)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);

            var lists = (await _toDoItemAppService.GetAsync(userId)).AsQueryable();
            return lists;
        }

        #endregion
        #region Todolists

        /// <summary>
        /// Get todolists.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        /// <returns>Returns todolists.</returns>
        [Authorize]
        [UsePaging(SchemaType = typeof(ListsType))]
        [UseFiltering]
        public async Task<IQueryable<ToDoListResponseDto>> Lists([Service] IHttpContextAccessor contextAccessor)
        {
            var userId = Convert.ToInt64(contextAccessor.HttpContext.Items["UserId"]);
            var lists = (await _toDoListAppService.GetAsync(userId)).AsQueryable();
            return lists;
        }

        #endregion

    }
}
