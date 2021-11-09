using AdForm.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormAssignment.DataService
{
    public interface IToDoListDataService
    {
        Task<List<ToDoLists>> GetAllAsync(long userId);
        Task<ToDoLists> GetByIdAsync(long listId, long userId);
        Task<ToDoLists> GetByNameAsync(string listName, long userId);
        Task<ToDoLists> AddAsync(ToDoLists item);
        Task<ToDoLists> UpdateAsync(ToDoLists item);
        Task<ToDoLists> DeleteAsync(ToDoLists item);
        Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument item);
    }
}
