﻿using AdForm.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdFormAssignment.DataService
{
    public interface ILabelDataService
    {
        Task<List<Labels>> GetAllAsync();
        Task<Labels> GetByIdAsync(long labelId);
        Task<Labels> GetByNameAsync(string labelName);
        Task<Labels> AddAsync(Labels label);
        Task<Labels> DeleteAsync(Labels label);
    }
}
