using AdForm.DBService;
using AdForm.Core;
using ToDoApp.DataService;
using ToDoApp.Shared;
using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace ToDoApp.Business
{
    public class LableAppService : ILableAppService
    {
        private readonly ILabelDataService _labelDataService;
        private readonly IMapper _mapper;

        public LableAppService(ILabelDataService labelDataService, IMapper mapper)
        {
            _labelDataService = labelDataService;
            _mapper = mapper;
        }

        public async Task<LabelResponseDto> CreateAsync(LabelRequestDto labelRequestDto)
        {
            var dbLabel = await _labelDataService.GetByNameAsync(labelRequestDto.Name, labelRequestDto.UserId);
            if (dbLabel != null)
                throw new ApiException(ErrorMessage.Label_Exist, HttpStatusCode.Conflict, ApiExceptionType.LabelAlreadyExists);
            var label = await _labelDataService.AddAsync(_mapper.Map<Labels>(labelRequestDto));
            return _mapper.Map<LabelResponseDto>(label);
        }

        public async Task DeleteAsync(long labelId, long userId)
        {
            var dbLabel = await _labelDataService.GetByIdAsync(labelId, userId);
            if (dbLabel == null)
                throw new ApiException(ErrorMessage.Label_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.LabelNotfound);
            await _labelDataService.DeleteAsync(dbLabel);
        }

        public async Task<PagedList<LabelResponseDto>> GetAsync(PaginationParameters pagination, long userId)
        {

            var labels = _mapper.Map<List<LabelResponseDto>>(await _labelDataService.GetAllAsync(userId));

            if (!string.IsNullOrWhiteSpace(pagination.SearchText))
            {
                labels = labels.Where(i => i.Name.Contains(pagination.SearchText)).ToList();
            }

            return PagedList<LabelResponseDto>.ToPagedList(labels, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<LabelResponseDto> GetAsync(long labelId, long userId)
        {
            var dbLabel = await _labelDataService.GetByIdAsync(labelId, userId);
            if (dbLabel == null)
                throw new ApiException(ErrorMessage.Label_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.LabelNotfound);

            return _mapper.Map<LabelResponseDto>(dbLabel);
        }

        public async Task<List<LabelResponseDto>> GetAsync(long userId)
        {
            var labels = _mapper.Map<List<LabelResponseDto>>(await _labelDataService.GetAllAsync(userId));
            if (labels.Count == 0)
                throw new ApiException(ErrorMessage.Label_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.LabelNotfound);
            return labels;
        }
    }
}
