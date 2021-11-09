using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormAssignment.Business
{
    public interface IToDoItemAppService
    {

        Task<PagedList<ToDoItemResponseDto>> GetAsync(PaginationParameters pagination, long userId);
        Task<ToDoItemResponseDto> GetAsync(long itemId, long userId);
        Task<ToDoItemResponseDto> CreateAsync(ToDoItemRequestDto createToDoItemRequest);
        Task UpdateAsync(long itemId, ToDoItemRequestDto updateToDoItemRequest);
        Task DeleteAsync(long itemId, long userId);
        Task UpdateToDoItemPatchAsync(long itemId, long userId, JsonPatchDocument item);
        Task<List<ToDoItemResponseDto>> GetAsync(long userId);
        Task<bool> AssignLabel(AssignLabelRequestDto assignLabelRequestDto);
    }
}
