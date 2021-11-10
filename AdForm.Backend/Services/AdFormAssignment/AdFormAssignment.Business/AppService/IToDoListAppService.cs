﻿using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormAssignment.Business
{
    public interface IToDoListAppService
    {
        Task<PagedList<ToDoListResponseDto>> GetAsync(PaginationParameters pagination, long userId);
        Task<ToDoListResponseDto> GetAsync(long listId, long userId);
        Task<ToDoListResponseDto> CreateAsync(ToDoListRequestDto createToDoListRequest);
        Task UpdateAsync(long listId , ToDoListRequestDto updateToDoListRequest);
        Task DeleteAsync(long listId, long userId);
        Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument list);
        Task<List<ToDoListResponseDto>> GetAsync(long userId);
        Task<bool> AssignLabel(AssignLabelRequestDto assignLabelRequestDto);
    }
}
