﻿using AdForm.Entities;
using AdForm.SDK;
using AdFormAssignment.DataService;
using AdFormAssignment.Shared;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdFormAssignment.Business
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
            var dbList = await _toDoListDataService.GetByNameAsync(createToDoListRequest.ListName , createToDoListRequest.UserId);
            if (dbList != null)
                throw new ApiException(ErrorMessage.List_Exist, HttpStatusCode.Conflict, ApiExceptionType.ItemAlreadyExists);
            var item = await _toDoListDataService.AddAsync(_mapper.Map<ToDoLists>(createToDoListRequest));
            return _mapper.Map<ToDoListResponseDto>(item);
        }

        public async Task DeleteAsync(long listId, long userId)
        {
            var dbList = await _toDoListDataService.GetByIdAsync(listId, userId);
            if (dbList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemAlreadyExists);
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
            var dbList = await _toDoListDataService.GetByIdAsync(listId,userId);
            if (dbList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemAlreadyExists);
            return _mapper.Map<ToDoListResponseDto>(dbList);
        }

        public async Task UpdateAsync(long listId, ToDoListRequestDto updateToDoListRequest)
        {
            var dbList = await _toDoListDataService.GetByIdAsync(listId, updateToDoListRequest.UserId);
            if (dbList == null)
                throw new ApiException(ErrorMessage.List_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemAlreadyExists);
            await _toDoListDataService.UpdateAsync(_mapper.Map<ToDoLists>(updateToDoListRequest));
        }

        public async Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument list)
        {
            await _toDoListDataService.UpdateToDoListPatchAsync(listId, userId, list);
        }
    }
}
