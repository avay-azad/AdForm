using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdFormAssignment.Business
{
    public interface ILableAppService
    {
        Task<LabelResponseDto> CreateAsync(LabelRequestDto labelRequestDto);
        Task DeleteAsync(long labelId);
        Task<List<LabelResponseDto>> GetAsync();
        Task<LabelResponseDto> GetAsync(long labelId);
    }
}
