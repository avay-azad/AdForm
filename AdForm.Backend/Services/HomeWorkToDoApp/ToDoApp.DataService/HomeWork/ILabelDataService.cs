using AdForm.DBService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.DataService
{
    public interface ILabelDataService
    {
        Task<List<Labels>> GetAllAsync(long userId);
        Task<Labels> GetByIdAsync(long labelId, long userId);
        Task<Labels> GetByNameAsync(string labelName, long userId);
        Task<Labels> AddAsync(Labels label);
        Task<Labels> DeleteAsync(Labels label);
    }
}
