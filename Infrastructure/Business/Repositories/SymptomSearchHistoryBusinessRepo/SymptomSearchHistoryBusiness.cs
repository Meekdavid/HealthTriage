using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces.Business;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Database;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Infrastructure.Business.Repositories.SymptomSearchHistoryBusinessRepo
{
    public class SymptomSearchHistoryBusiness : ISymptomSearchHistoryBusiness
    {
        private readonly ILogger<SymptomSearchHistoryBusiness> _logger;
        private readonly ISymptomSearchHistoryRepository _symptomSearchHistoryRepo;
        private readonly IMapper _mapper;
        public SymptomSearchHistoryBusiness(ILogger<SymptomSearchHistoryBusiness> logger, ISymptomSearchHistoryRepository symptomSearchHistoryRepo, IMapper mapper)
        {
            _logger = logger;
            _symptomSearchHistoryRepo = symptomSearchHistoryRepo;
            _mapper = mapper;
        }
        public async Task<IDataResult<PaginatedList<SymptomSearchHistoryResponseDto>>> RetrieveSymptomSearchHistoryForSpecificUser(int pageIndex, int pageSize, string userId)
        {
            _logger.LogInformation($"About Retrieving all Symptom Search Histories with pageNumber: {pageIndex} and pageSize {pageSize}");

            var symtpomSearchHistories = await _symptomSearchHistoryRepo.RetrieveSymptomSearchHistoryForSpecificUser(userId);

            var paginatedResult = await PaginatedList<SymptomSearchHistoryResponseDto>
                .CreateAsync(symtpomSearchHistories
                .ProjectTo<SymptomSearchHistoryResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<SymptomSearchHistoryResponseDto>>(paginatedResult);
        }
    }
}
