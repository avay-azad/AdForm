using AdForm.DBService;
using AdForm.Core;
using ToDoApp.DataService;
using ToDoApp.Shared;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ToDoApp.Business
{
    public class ToDoItemAppService : IToDoItemAppService
    {
        private readonly IToDoItemDataService _toDoItemDataService;
        private readonly IToDoListDataService _toDoListDataService;
        private readonly IMapper _mapper;

        public ToDoItemAppService(IToDoItemDataService toDoItemDataService, IToDoListDataService toDoListDataService, IMapper mapper)
        {
            _toDoItemDataService = toDoItemDataService;
            _toDoListDataService = toDoListDataService;
            _mapper = mapper;
        }
        public async Task<ToDoItemResponseDto> CreateAsync(ToDoItemRequestDto createToDoItemRequest)
        {
            var dbItem = await _toDoItemDataService.GetByNameAsync(createToDoItemRequest.ItemName, createToDoItemRequest.UserId);
            if (dbItem != null)
                throw new ApiException(ErrorMessage.Item_Exist, HttpStatusCode.Conflict, ApiExceptionType.ToDoItemAlreadyExists);

            var toDoList = await _toDoListDataService.GetByIdAsync(createToDoItemRequest.ToDoListId, createToDoItemRequest.UserId);

            if (toDoList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ToDoListNotfound);

            var item = await _toDoItemDataService.AddAsync(_mapper.Map<ToDoItems>(createToDoItemRequest));
            return _mapper.Map<ToDoItemResponseDto>(item);

        }

        public async Task DeleteAsync(long itemId, long userId)
        {
            var dbItem = await _toDoItemDataService.GetByIdAsync(itemId, userId);
            if (dbItem == null)
                throw new ApiException(ErrorMessage.Item_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ToDoItemNotfound);
            await _toDoItemDataService.DeleteAsync(dbItem);
        }

        public async Task<PagedList<ToDoItemResponseDto>> GetAllAsync(PaginationParameters pagination, long userId)
        {
            var toDoItems = _mapper.Map<List<ToDoItemResponseDto>>(await _toDoItemDataService.GetAllAsync(userId));

            if (!string.IsNullOrWhiteSpace(pagination.SearchText))
            {
                toDoItems = toDoItems.Where(i => i.Name.Contains(pagination.SearchText)).ToList();
            }

            return PagedList<ToDoItemResponseDto>.ToPagedList(toDoItems, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<ToDoItemResponseDto> GetByIdAsync(long itemId, long userId)
        {
            var dbItem = await _toDoItemDataService.GetByIdAsync(itemId, userId);
            if (dbItem == null)
                throw new ApiException(ErrorMessage.Item_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ToDoItemNotfound);

            return _mapper.Map<ToDoItemResponseDto>(dbItem);
        }

        public async Task<List<ToDoItemResponseDto>> GetAsync(long userId)
        {
            var toDoItems = _mapper.Map<List<ToDoItemResponseDto>>(await _toDoItemDataService.GetAllAsync(userId));
            return toDoItems;
        }

        public async Task UpdateAsync(long itemId, UpdateToDoItemRequestDto updateToDoItemRequest)
        {
            await GetByIdAsync(itemId, updateToDoItemRequest.UserId);
            await _toDoItemDataService.UpdateAsync(_mapper.Map<ToDoItems>(updateToDoItemRequest));
        }

        public async Task UpdateToDoItemPatchAsync(long itemId, long userId, JsonPatchDocument item)
        {
            await _toDoItemDataService.UpdateItemPatchAsync(itemId, userId, item);
        }

        public async Task<bool> AssignLabel(long toDoItemId, AssignLabelRequestDto assignLabelRequestDto, ILableAppService lableAppService)
        {
            await GetByIdAsync(toDoItemId, assignLabelRequestDto.UserId);
            List<LabelToDoItem> lstLabelToDoItem = new List<LabelToDoItem>();
            if (assignLabelRequestDto.LabelId.Count > 0)
            {
                var assignedLabel = await _toDoItemDataService.GetAssignedLabelAsync(toDoItemId);
                for (int i = 0; i <= assignLabelRequestDto.LabelId.Count - 1; i++)
                {
                    await lableAppService.GetByIdAsync(assignLabelRequestDto.LabelId[i], assignLabelRequestDto.UserId);
                    if (assignedLabel.Count > 0)
                    {
                        var allreadryAssigned = assignedLabel.FirstOrDefault(l => l.LabelId == assignLabelRequestDto.LabelId[i]);
                        if (allreadryAssigned == null)
                        {
                            lstLabelToDoItem.Add(new LabelToDoItem
                            {
                                LabelId = assignLabelRequestDto.LabelId[i],
                                ToDoItemId = toDoItemId
                            });
                        }
                    }
                    else
                    {
                        lstLabelToDoItem.Add(new LabelToDoItem
                        {
                            LabelId = assignLabelRequestDto.LabelId[i],
                            ToDoItemId = toDoItemId
                        });
                    }
                }
            }

            LabelToDoItem[] labelToDoItems = lstLabelToDoItem.ToArray<LabelToDoItem>();

            await _toDoItemDataService.AssignLabel(labelToDoItems);
            return true;
        }
    }
}
