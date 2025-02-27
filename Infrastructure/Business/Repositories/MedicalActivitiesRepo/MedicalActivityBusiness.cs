using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.DTOs;
using Common.Enums;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces.Business;
using Domain.Interfaces.Database;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Repositories.MedicalActivitiesRepo
{
    public class MedicalActivityBusiness : IMedicalActivityBusiness
    {
        private readonly IMedicalActivityRepository _medicalActivityRepo;
        private readonly ILogger<MedicalActivityBusiness> _logger;
        private readonly IMapper _mapper;
        public MedicalActivityBusiness(IMedicalActivityRepository medicalActivityRepo, ILogger<MedicalActivityBusiness> logger, IMapper mapper)
        {
            _medicalActivityRepo = medicalActivityRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveAllMedicalActivities(int pageIndex, int pageSize)
        {
            _logger.LogInformation($"About Retrieving all Medical Activities with pageNumber: {pageIndex} and pageSize {pageSize}");

            var medicalActivities = await _medicalActivityRepo.RetrieveAllMedicalActivities();

            var paginatedResult = await PaginatedList<MedicalActivityLogResponseDto>
                .CreateAsync(medicalActivities
                .ProjectTo<MedicalActivityLogResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<MedicalActivityLogResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveMedicalActivitiesByUserType(int pageIndex, int pageSize, AuthorType userType)
        {
            _logger.LogInformation($"About Retrieving all Medical Activities with pageNumber: {pageIndex} and pageSize {pageSize} and User Type {userType}");

            var medicalActivities = await _medicalActivityRepo.RetrieveMedicalActivitiesByUserType(userType);

            var paginatedResult = await PaginatedList<MedicalActivityLogResponseDto>
                .CreateAsync(medicalActivities
                .ProjectTo<MedicalActivityLogResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<MedicalActivityLogResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveMedicalActivitiesForSpecificDateRange(int pageIndex, int pageSize, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"About Retrieving all Medical Activities with pageNumber: {pageIndex} and pageSize {pageSize} and Date Range {startDate} to {endDate}");

            var medicalActivities = await _medicalActivityRepo.RetrieveMedicalActivitiesForSpecificDateRange(startDate, endDate);

            var paginatedResult = await PaginatedList<MedicalActivityLogResponseDto>
                .CreateAsync(medicalActivities
                .ProjectTo<MedicalActivityLogResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<MedicalActivityLogResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveMedicalActivitiesForSpecificUser(int pageIndex, int pageSize, string userId)
        {
            _logger.LogInformation($"About Retrieving all Medical Activities with pageNumber: {pageIndex} and pageSize {pageSize} and for Speciic User {userId}");

            var medicalActivities = await _medicalActivityRepo.RetrieveMedicalActivitiesForSpecificUser(userId);

            var paginatedResult = await PaginatedList<MedicalActivityLogResponseDto>
                .CreateAsync(medicalActivities
                .ProjectTo<MedicalActivityLogResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<MedicalActivityLogResponseDto>>(paginatedResult);
        }
    }
}
