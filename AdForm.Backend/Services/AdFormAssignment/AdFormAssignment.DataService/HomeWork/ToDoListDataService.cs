using AdForm.DBService;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdFormAssignment.DataService
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

        public async Task<ToDoLists> DeleteAsync(ToDoLists list)
        {
            _dbContext.ToDoLists.Remove(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }

        public async Task<List<ToDoLists>> GetAllAsync(long userId)
        {
            return await _dbContext.ToDoLists.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<ToDoLists> GetByIdAsync(long listId, long userId)
        {
            var result = await _dbContext.ToDoLists.FirstOrDefaultAsync(i => i.Id == listId && i.UserId == userId);
            return result;
        }

        public async Task<ToDoLists> GetByNameAsync(string listName, long userId)
        {
            var result = await _dbContext.ToDoLists.FirstOrDefaultAsync(i => i.Name == listName && i.UserId == userId);
            return result;
        }


        public async Task<ToDoLists> UpdateAsync(ToDoLists list)
        {
            await _dbContext.SaveChangesAsync();
            return list;
        }

        public async Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument item)
        {
            var toDoList = await _dbContext.ToDoLists.FirstOrDefaultAsync(i => i.Id == listId && i.UserId == userId);
            if (toDoList != null)
            {
                item.ApplyTo(toDoList);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
