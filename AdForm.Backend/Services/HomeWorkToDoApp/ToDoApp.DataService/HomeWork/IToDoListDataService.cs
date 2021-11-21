using AdForm.DBService;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.DataService
{
    public interface IToDoListDataService
    {
        Task<List<ToDoLists>> GetAllAsync(long userId);
        Task<ToDoLists> GetByIdAsync(long listId, long userId);
        Task<ToDoLists> GetByNameAsync(string listName, long userId);
        Task<ToDoLists> AddAsync(ToDoLists list);
        Task<ToDoLists> UpdateAsync(ToDoLists list);
        Task<ToDoLists> DeleteAsync(ToDoLists list);
        Task UpdateToDoListPatchAsync(long listId, long userId, JsonPatchDocument item);
    }
}
