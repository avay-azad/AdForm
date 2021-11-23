using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Business
{
    public interface ILableAppService
    {
        Task<LabelResponseDto> CreateAsync(LabelRequestDto labelRequestDto);
        Task DeleteAsync(long labelId, long userId);
        Task<PagedList<LabelResponseDto>> GetAsync(PaginationParameters pagination, long userId);
        Task<LabelResponseDto> GetAsync(long labelId, long userId);
        Task<List<LabelResponseDto>> GetAsync(long userId);
    }
}
