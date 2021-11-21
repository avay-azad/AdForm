using AdForm.DBService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.Labels.Remove(label);
            await _dbContext.SaveChangesAsync();
            return label;
        }

        public async Task<List<Labels>> GetAllAsync()
        {
            return await _dbContext.Labels.ToListAsync();
        }

        public async Task<Labels> GetByIdAsync(long labelId)
        {
            var result = await _dbContext.Labels.FirstOrDefaultAsync(i => i.Id == labelId);
            return result;
        }

        public async Task<Labels> GetByNameAsync(string labelName)
        {
            var result = await _dbContext.Labels.FirstOrDefaultAsync(i => i.Name == labelName);
            return result;
        }
    }
}
