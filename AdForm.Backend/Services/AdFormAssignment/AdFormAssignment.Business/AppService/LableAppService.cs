using AdForm.Entities;
using AdForm.SDK;
using AdFormAssignment.DataService;
using AdFormAssignment.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdFormAssignment.Business
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
            var dbLabel = await _labelDataService.GetByNameAsync(labelRequestDto.Name);
            if (dbLabel != null)
                throw new ApiException(ErrorMessage.Label_Exist, HttpStatusCode.Conflict, ApiExceptionType.ItemAlreadyExists);
            var label = await _labelDataService.AddAsync(_mapper.Map<Labels>(labelRequestDto));
            return _mapper.Map<LabelResponseDto>(label);
        }

        public async Task DeleteAsync(long labelId)
        {
            var dbLabel = await _labelDataService.GetByIdAsync(labelId);
            if (dbLabel == null)
                throw new ApiException(ErrorMessage.Label_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            await _labelDataService.DeleteAsync(dbLabel);
        }

        public async Task<List<LabelResponseDto>> GetAsync()
        {
            var labels = _mapper.Map<List<LabelResponseDto>>(await _labelDataService.GetAllAsync());
            if(labels.Count ==0)
                throw new ApiException(ErrorMessage.Label_Not_Exist, HttpStatusCode.NotFound, ApiExceptionType.ItemNotfound);
            return labels;
        }

        public async Task<LabelResponseDto> GetAsync(long labelId)
        {
            var dbLabel = await _labelDataService.GetByIdAsync(labelId);
            return _mapper.Map<LabelResponseDto>(dbLabel);
        }
    }
}
