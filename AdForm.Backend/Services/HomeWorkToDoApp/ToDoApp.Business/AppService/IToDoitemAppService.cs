using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Business
{
    public interface IToDoItemAppService
    {

        Task<PagedList<ToDoItemResponseDto>> GetAllAsync(PaginationParameters pagination, long userId);
        Task<ToDoItemResponseDto> GetByIdAsync(long itemId, long userId);
        Task<ToDoItemResponseDto> CreateAsync(ToDoItemRequestDto createToDoItemRequest);
        Task UpdateAsync(long itemId, UpdateToDoItemRequestDto updateToDoItemRequest);
        Task DeleteAsync(long itemId, long userId);
        Task UpdateToDoItemPatchAsync(long itemId, long userId, JsonPatchDocument item);
        Task<List<ToDoItemResponseDto>> GetAsync(long userId);
        Task<bool> AssignLabel(long toDoItemId, AssignLabelRequestDto assignLabelRequestDto, ILableAppService lableAppService);
    }
}
