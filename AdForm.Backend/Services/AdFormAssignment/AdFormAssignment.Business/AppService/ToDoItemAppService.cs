using AdForm.Entities;
using AdForm.SDK;
using AdFormAssignment.DataService;
using AdFormAssignment.Shared;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdFormAssignment.Business
{
    public class ToDoItemAppService : IToDoItemAppService
    {
        private readonly IToDoItemDataService _toDoItemDataService;
        private readonly IMapper _mapper;

        public ToDoItemAppService(IToDoItemDataService toDoItemDataService, IMapper mapper)
        {
            _toDoItemDataService = toDoItemDataService;
            _mapper = mapper;
        }
        public async Task<ToDoItemResponseDto> CreateAsync(ToDoItemRequestDto createToDoItemRequest)
        {
            var dbItem = await _toDoItemDataService.GetByNameAsync(createToDoItemRequest.ItemName, createToDoItemRequest.UserId);
            if (dbItem != null)
                throw new ApiException(ErrorMessage.Item_Exist, HttpStatusCode.Conflict, ApiExceptionType.ItemAlreadyExists);
            var item = await _toDoItemDataService.AddAsync(_mapper.Map<ToDoItems>(createToDoItemRequest));
            return _mapper.Map<ToDoItemResponseDto>(item);

        }

        public async Task DeleteAsync(long itemId, long userId)
        {
            var dbItem = await _toDoItemDataService.GetByIdAsync(itemId, userId);
            if (dbItem == null)
                throw new ApiException(ErrorMessage.Item_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            await _toDoItemDataService.DeleteAsync(dbItem);
        }

        public async Task<PagedList<ToDoItemResponseDto>> GetAsync(PaginationParameters pagination, long userId)
        {
            var toDoItems = _mapper.Map<List<ToDoItemResponseDto>>(await _toDoItemDataService.GetAllAsync(userId));

            if (!string.IsNullOrWhiteSpace(pagination.SearchText))
            {
                toDoItems = toDoItems.Where(i => i.Name.Contains(pagination.SearchText)).ToList();
            }

            return PagedList<ToDoItemResponseDto>.ToPagedList(toDoItems, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<ToDoItemResponseDto> GetAsync(long itemId, long userId)
        {
            var dbItem = await _toDoItemDataService.GetByIdAsync(itemId, userId);
            return _mapper.Map<ToDoItemResponseDto>(dbItem);
        }

        public async Task<List<ToDoItemResponseDto>> GetAsync(long userId)
        {
            var toDoItems = _mapper.Map<List<ToDoItemResponseDto>>(await _toDoItemDataService.GetAllAsync(userId));
            return toDoItems;
        }

        public async Task UpdateAsync(long itemId, ToDoItemRequestDto updateToDoItemRequest)
        {
            var dbItem = await _toDoItemDataService.GetByIdAsync(itemId, updateToDoItemRequest.UserId);
            if (dbItem == null)
                throw new ApiException(ErrorMessage.Item_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            await _toDoItemDataService.UpdateAsync(_mapper.Map<ToDoItems>(updateToDoItemRequest));
        }

        public async Task UpdateToDoItemPatchAsync(long itemId, long userId, JsonPatchDocument item)
        {
            await _toDoItemDataService.UpdateItemPatchAsync(itemId, userId, item);
        }

        public async Task<bool> AssignLabel(AssignLabelRequestDto assignLabelRequestDto)
        {
            var dbItem = await _toDoItemDataService.GetByIdAsync(assignLabelRequestDto.EntityId, assignLabelRequestDto.UserId);
            if (dbItem == null)
                throw new ApiException(ErrorMessage.Item_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);

            dbItem.LabelId = assignLabelRequestDto.LabelId;
            await _toDoItemDataService.UpdateAsync(dbItem);
            return true;
        }
    }
}
