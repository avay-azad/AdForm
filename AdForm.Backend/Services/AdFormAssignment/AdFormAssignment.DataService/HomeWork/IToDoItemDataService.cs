using AdForm.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormAssignment.DataService
{
    public interface IToDoItemDataService
    {
        Task<List<ToDoItems>> GetAllAsync(long userId);
        Task<ToDoItems> GetByIdAsync(long itemId, long userId);
        Task<ToDoItems> GetByNameAsync(string itemName, long userId);
        Task<ToDoItems> AddAsync(ToDoItems item);
        Task<ToDoItems> UpdateAsync(ToDoItems item);
        Task<ToDoItems> DeleteAsync(ToDoItems item);
        Task UpdateItemPatchAsync(long itemId, long userId, JsonPatchDocument item);
    }
}
