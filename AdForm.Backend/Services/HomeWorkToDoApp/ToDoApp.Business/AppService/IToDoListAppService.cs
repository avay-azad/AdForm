using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Business
{
    public interface IToDoListAppService
    {
        Task<PagedList<ToDoListResponseDto>> GetAllAsync(PaginationParameters pagination, long userId);
        Task<ToDoListResponseDto> GetByIdAsync(long listId, long userId);
        Task<ToDoListResponseDto> CreateAsync(ToDoListRequestDto createToDoListRequest);
        Task UpdateAsync(long listId , ToDoListRequestDto updateToDoListRequest);
        Task DeleteAsync(long listId, long userId);
        Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument list);
        Task<List<ToDoListResponseDto>> GetAsync(long userId);
        Task<bool> AssignLabel(long toDoListId, AssignLabelRequestDto assignLabelRequestDto, ILableAppService lableAppService);
      
    }
}
