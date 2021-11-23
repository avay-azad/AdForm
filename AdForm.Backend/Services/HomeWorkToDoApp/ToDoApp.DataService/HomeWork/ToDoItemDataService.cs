using AdForm.DBService;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.DataService
{
    public class ToDoItemDataService : IToDoItemDataService
    {
        private readonly HomeworkDBContext _dbContext;

        public ToDoItemDataService(HomeworkDBContext homeworkDBContext)
        {
            _dbContext = homeworkDBContext;
        }

        public async Task<ToDoItems> AddAsync(ToDoItems item)
        {
            _dbContext.ToDoItems.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task AssignLabel(LabelToDoItem[] labelToDoItems)
        {
            await _dbContext.LabelToDoItems.AddRangeAsync(labelToDoItems);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LabelToDoItem>> GetAssignedLabelAsync(long toDoItemId)
        {
            return await _dbContext.LabelToDoItems.Where(l => l.ToDoItemId == toDoItemId).ToListAsync();
        }

        public async Task<ToDoItems> DeleteAsync(ToDoItems item)
        {
            var mappedLable = _dbContext.LabelToDoItems.Where(i => i.ToDoItemId == item.ToDoItemId).ToList();
            if (mappedLable.Count > 0)
            {
                _dbContext.LabelToDoItems.RemoveRange(mappedLable);
            }
            _dbContext.ToDoItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<List<ToDoItems>> GetAllAsync(long userId)
        {
            return await _dbContext.ToDoItems.Where(i => i.UserId == userId).Include(l => l.LabelToDoItems).ToListAsync();
        }

        public async Task<ToDoItems> GetByIdAsync(long itemId, long userId)
        {
            var result = await _dbContext.ToDoItems.Include(l => l.LabelToDoItems).FirstOrDefaultAsync(i => i.ToDoItemId == itemId && i.UserId == userId);
            return result;
        }

        public async Task<ToDoItems> GetByNameAsync(string itemName, long userId)
        {
            var result = await _dbContext.ToDoItems.FirstOrDefaultAsync(i => i.Name == itemName && i.UserId == userId);
            return result;
        }

        public async Task<ToDoItems> UpdateAsync(ToDoItems item)
        {
            item.UpdatedDate= DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return item;
        }


        public async Task UpdateItemPatchAsync(long itemId, long userId, JsonPatchDocument item)
        {
            var toDoItem = await _dbContext.ToDoItems.FirstOrDefaultAsync(i => i.ToDoItemId == itemId && i.UserId == userId);
            if (toDoItem != null)
            {
                toDoItem.UpdatedDate = DateTime.UtcNow;
                item.ApplyTo(toDoItem);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
