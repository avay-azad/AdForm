using AdForm.DBService;
using AdForm.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdFormAssignment.DataService
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

        public async Task<ToDoItems> DeleteAsync(ToDoItems item)
        {
            _dbContext.ToDoItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<List<ToDoItems>> GetAllAsync(long userId)
        {
            return await _dbContext.ToDoItems.Where(i=>i.UserId == userId).ToListAsync();
        }

        public async Task<ToDoItems> GetByIdAsync(long itemId, long userId)
        {
            var result = await _dbContext.ToDoItems.FirstOrDefaultAsync(i=>i.Id == itemId && i.UserId == userId);
            return result;
        }

        public async Task<ToDoItems> GetByNameAsync(string itemName, long userId)
        {
            var result = await _dbContext.ToDoItems.FirstOrDefaultAsync(i => i.Name == itemName && i.UserId == userId);
            return result;
        }

        public async Task<ToDoItems> UpdateAsync(ToDoItems item)
        {
            await _dbContext.SaveChangesAsync();
            return item;
        }


        public async Task UpdateItemPatchAsync(long itemId, long userId, JsonPatchDocument item)
        {
            var toDoItem = await _dbContext.ToDoItems.FirstOrDefaultAsync(i => i.Id == itemId && i.UserId == userId);
            if(toDoItem != null)
            {
                item.ApplyTo(toDoItem);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
