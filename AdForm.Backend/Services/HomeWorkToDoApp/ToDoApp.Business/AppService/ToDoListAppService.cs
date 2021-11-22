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
    public class ToDoListAppService : IToDoListAppService
    {
        private readonly IToDoListDataService _toDoListDataService;
        private readonly IMapper _mapper;

        public ToDoListAppService(IToDoListDataService toDoListDataService, IMapper mapper)
        {
            _toDoListDataService = toDoListDataService;
            _mapper = mapper;
        }

        public async Task<ToDoListResponseDto> CreateAsync(ToDoListRequestDto createToDoListRequest)
        {
            var dbList = await _toDoListDataService.GetByNameAsync(createToDoListRequest.ListName, createToDoListRequest.UserId);
            if (dbList != null)
                throw new ApiException(ErrorMessage.List_Exist, HttpStatusCode.Conflict, ApiExceptionType.ItemAlreadyExists);
            var item = await _toDoListDataService.AddAsync(_mapper.Map<ToDoLists>(createToDoListRequest));
            return _mapper.Map<ToDoListResponseDto>(item);
        }

        public async Task DeleteAsync(long listId, long userId)
        {
            var dbList = await _toDoListDataService.GetByIdAsync(listId, userId);
            if (dbList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            await _toDoListDataService.DeleteAsync(dbList);
        }

        public async Task<PagedList<ToDoListResponseDto>> GetAsync(PaginationParameters pagination, long userId)
        {
            var toDoLists = _mapper.Map<List<ToDoListResponseDto>>(await _toDoListDataService.GetAllAsync(userId));

            if (!string.IsNullOrWhiteSpace(pagination.SearchText))
            {
                toDoLists = toDoLists.Where(i => i.Name.Contains(pagination.SearchText)).ToList();
            }

            return PagedList<ToDoListResponseDto>.ToPagedList(toDoLists, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<ToDoListResponseDto> GetAsync(long listId, long userId)
        {
            var dbList = await _toDoListDataService.GetByIdAsync(listId, userId);
            if (dbList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            return _mapper.Map<ToDoListResponseDto>(dbList);
        }

        public async Task<List<ToDoListResponseDto>> GetAsync(long userId)
        {
            var toDoLists = _mapper.Map<List<ToDoListResponseDto>>(await _toDoListDataService.GetAllAsync(userId));
            return toDoLists;
        }

        public async Task UpdateAsync(long listId, ToDoListRequestDto updateToDoListRequest)
        {
            var dbList = await _toDoListDataService.GetByIdAsync(listId, updateToDoListRequest.UserId);
            if (dbList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            await _toDoListDataService.UpdateAsync(_mapper.Map<ToDoLists>(updateToDoListRequest));
        }

        public async Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument list)
        {
            await _toDoListDataService.UpdateToDoListPatchAsync(listId, userId, list);
        }
        public async Task<bool> AssignLabel(long toDoListId, AssignLabelRequestDto assignLabelRequestDto, ILableAppService lableAppService)
        {
            List<LabelToDoList> lstLabelToDoList = new List<LabelToDoList>();
            if (assignLabelRequestDto.LabelId.Count > 0)
            {
                for(int i = 0; i <= assignLabelRequestDto.LabelId.Count-1; i++)
                {

                    lstLabelToDoList.Add(new LabelToDoList { LabelId = assignLabelRequestDto.LabelId[i], ToDoListId = toDoListId });
                }
            }

            LabelToDoList[] labelToDoLists = lstLabelToDoList.ToArray<LabelToDoList>();

             await _toDoListDataService.AssignLabel(labelToDoLists);

            return true;
        }
    }
}
