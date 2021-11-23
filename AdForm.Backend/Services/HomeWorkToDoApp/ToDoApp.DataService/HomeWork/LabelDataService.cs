using AdForm.DBService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ToDoApp.DataService
{
    public class LabelDataService : ILabelDataService
    {
        private readonly HomeworkDBContext _dbContext;

        public LabelDataService(HomeworkDBContext homeworkDBContext)
        {
            _dbContext = homeworkDBContext;
        }
        public async Task<Labels> AddAsync(Labels label)
        {
            _dbContext.Labels.Add(label);
            await _dbContext.SaveChangesAsync();
            return label;
        }

        public async Task<Labels> DeleteAsync(Labels label)
        {
            var mappedList = _dbContext.LabelToDoLists.Where(l => l.LabelId == label.LabelId).ToList();
            if (mappedList.Count > 0)
            {
                _dbContext.LabelToDoLists.RemoveRange(mappedList);
            }
            var mappedItem = _dbContext.LabelToDoItems.Where(l => l.LabelId == label.LabelId).ToList();
            if (mappedItem.Count > 0)
            {
                _dbContext.LabelToDoItems.RemoveRange(mappedItem);
            }
            _dbContext.Labels.Remove(label);
            await _dbContext.SaveChangesAsync();
            return label;
        }

        public async Task<List<Labels>> GetAllAsync(long userId)
        {
            return await _dbContext.Labels.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Labels> GetByIdAsync(long labelId, long userId)
        {
            var result = await _dbContext.Labels.FirstOrDefaultAsync(i => i.LabelId == labelId && i.UserId == userId);
            return result;
        }

        public async Task<Labels> GetByNameAsync(string labelName, long userId)
        {
            var result = await _dbContext.Labels.FirstOrDefaultAsync(i => i.Name == labelName && i.UserId == userId);
            return result;
        }
    }
}
