using AdForm.DBService;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.DataService
{
    public class ToDoListDataService : IToDoListDataService
    {
        private readonly HomeworkDBContext _dbContext;

        public ToDoListDataService(HomeworkDBContext homeworkDBContext)
        {
            _dbContext = homeworkDBContext;
        }

        public async Task<ToDoLists> AddAsync(ToDoLists list)
        {
            _dbContext.ToDoLists.Add(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }

        public async Task AssignLabel(LabelToDoList[] labelToDoList)
        {
            await _dbContext.LabelToDoLists.AddRangeAsync(labelToDoList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ToDoLists> DeleteAsync(ToDoLists list)
        {
            var mappedLable = _dbContext.LabelToDoLists.Where(l => l.ToDoListId == list.ToDoListId).ToList();
            if (mappedLable.Count > 0)
            {
                _dbContext.LabelToDoLists.RemoveRange(mappedLable);
            }
            _dbContext.ToDoLists.Remove(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }

        public async Task<List<ToDoLists>> GetAllAsync(long userId)
        {
            return await _dbContext.ToDoLists.Where(i => i.UserId == userId).Include(l => l.LabelToDoLists).ToListAsync();
        }

        public async Task<List<LabelToDoList>> GetAssignedLabelAsync(long toDoListId)
        {
            return await _dbContext.LabelToDoLists.Where(l => l.ToDoListId == toDoListId).ToListAsync();
        }

        public async Task<ToDoLists> GetByIdAsync(long listId, long userId)
        {
            var result = await _dbContext.ToDoLists.Include(l => l.LabelToDoLists).FirstOrDefaultAsync(i => i.ToDoListId == listId && i.UserId == userId);
            return result;
        }

        public async Task<ToDoLists> GetByNameAsync(string listName, long userId)
        {
            var result = await _dbContext.ToDoLists.FirstOrDefaultAsync(i => i.Name == listName && i.UserId == userId);
            return result;
        }


        public async Task<ToDoLists> UpdateAsync(ToDoLists list)
        {
            list.UpdatedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return list;
        }

        public async Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument item)
        {
            var toDoList = await _dbContext.ToDoLists.FirstOrDefaultAsync(i => i.ToDoListId == listId && i.UserId == userId);
            if (toDoList != null)
            {
                toDoList.UpdatedDate = DateTime.UtcNow;
                item.ApplyTo(toDoList);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
