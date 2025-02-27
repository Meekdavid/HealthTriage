using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces.Business;
using Domain.Interfaces.Database;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Infrastructure.Business.Repositories.ConsultationHistoryBusiness
{
    public class ConsultationHistoryBusiness : IConsultationHistoryBusiness
    {
        private readonly ILogger<ConsultationHistoryBusiness> _logger;
        private readonly IConsultationHistoryRepository _consultationHistoryRepo;
        private readonly IMapper _mapper;

        public ConsultationHistoryBusiness(ILogger<ConsultationHistoryBusiness> logger, IConsultationHistoryRepository consultationHistoryRepo, IMapper mapper)
        {
            _logger = logger;
            _consultationHistoryRepo = consultationHistoryRepo;
            _mapper = mapper;
        }
        public async Task<IDataResult<PaginatedList<ConsultationHistoryResponseDto>>> RetrieveAllConsultationHistory(int pageIndex, int pageSize)
        {
            _logger.LogInformation($"About Retrieving all Consultation Histories with pageNumber: {pageIndex} and pageSize {pageSize}");

            var consultations = await _consultationHistoryRepo.RetrieveAllConsultationHistory();

            var paginatedResult = await PaginatedList<ConsultationHistoryResponseDto>
                .CreateAsync(consultations
                .ProjectTo<ConsultationHistoryResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<ConsultationHistoryResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<ConsultationHistoryResponseDto>>> RetrieveConsultationHistoryForSpecificDateRange(int pageIndex, int pageSize, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"About Retrieving all Consultation Histories with pageNumber: {pageIndex} and pageSize {pageSize} for specific date range {startDate} to {endDate}");

            var consultations = await _consultationHistoryRepo.RetrieveConsultationHistoryForSpecificDateRange(startDate, endDate);

            var paginatedResult = await PaginatedList<ConsultationHistoryResponseDto>
                .CreateAsync(consultations
                .ProjectTo<ConsultationHistoryResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<ConsultationHistoryResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<ConsultationHistoryResponseDto>>> RetrieveConsultationsForSpecificUser(int pageIndex, int pageSize, string userId)
        {
            _logger.LogInformation($"About Retrieving all Consultation Histories with pageNumber: {pageIndex} and pageSize {pageSize}");

            var consultations = await _consultationHistoryRepo.RetrieveConsultationsForSpecificUser(userId);

            var paginatedResult = await PaginatedList<ConsultationHistoryResponseDto>
                .CreateAsync(consultations
                .ProjectTo<ConsultationHistoryResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<ConsultationHistoryResponseDto>>(paginatedResult);
        }
    }
}
