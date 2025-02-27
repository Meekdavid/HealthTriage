using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.ConfigurationSettings;
using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces.Business;
using Domain.Interfaces.Database;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Business.Repositories.DropDownRepo
{
    public class DropdownBusiness : IDropdownBusiness
    {
        private readonly ICountryRepository _countryService;
        private readonly ILanguageRepository _languageService;
        private readonly IApiClient _apiCall;
        private readonly ILogger<DropdownBusiness> _logger;
        private readonly IMapper _mapper;

        public DropdownBusiness(ICountryRepository countryService,
            ILanguageRepository languageService,
            IApiClient apiCall,
            ILogger<DropdownBusiness> logger,
            IMapper mapper)
        {
            _countryService = countryService;
            _languageService = languageService;
            _apiCall = apiCall;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IDataResult<PaginatedList<string>>> CitiesOfCountry(int pageIndex, int pageSize, string country)
        {
            var requestPayload = new CitiesOfSpecifiedCountry
            {
                country = country
            };

            string citiesOfCountryUrl = ConfigSettings.ApplicationSetting.CitiesofSpecifiedCountry;

            var jsonPayload = JsonConvert.SerializeObject(requestPayload);

            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " },
                { "Accept", "application/json" }
            };

            var response = await _apiCall.PostAsync(citiesOfCountryUrl, httpContent, headers);

            if (!string.IsNullOrEmpty(response))
            {
                var deserializedResult = JsonConvert.DeserializeObject<CitiesOfSpecifiedCountryResponse>(response);

                var paginatedResult = await PaginatedList<string>
                .CreateAsync(deserializedResult.data.Select(t => t).ToList(), pageIndex, pageSize);

                return new SuccessDataResult<PaginatedList<string>>(paginatedResult);
            }
            else
            {
                return new ErrorDataResult<PaginatedList<string>>(null, StatusCode_NoCitiesFound, "No Cities Found");
            }
        }

        public async Task<IDataResult<PaginatedList<string>>> CitiesOfState(int pageIndex, int pageSize, string country, string state)
        {
            var requestPayload = new CitiesOfSpecifiedState
            {
                country = country,
                state = state
            };

            string citiesOfCountryUrl = ConfigSettings.ApplicationSetting.CitiesofSpecifiedState;

            var jsonPayload = JsonConvert.SerializeObject(requestPayload);

            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " },
                { "Accept", "application/json" }
            };

            var response = await _apiCall.PostAsync(citiesOfCountryUrl, httpContent, headers);

            if (!string.IsNullOrEmpty(response))
            {
                var deserializedResult = JsonConvert.DeserializeObject<CitiesOfSpecifiedStateResponse>(response);

                var paginatedResult = await PaginatedList<string>
                .CreateAsync(deserializedResult.data.Select(t => t).ToList(), pageIndex, pageSize);

                return new SuccessDataResult<PaginatedList<string>>(paginatedResult);
            }
            else
            {
                return new ErrorDataResult<PaginatedList<string>>(null, StatusCode_NoCitiesFound, "No Cities Found");
            }
        }

        public async Task<IDataResult<PaginatedList<CountryResponseDTO>>> RetrieveAllCountries(int pageIndex, int pageSize)
        {
            _logger.LogInformation($"About Retrieving Review all Countries with pageNumber: {pageIndex} and pageSize {pageSize}");

            var practitioners = await _countryService.GetAllCountries();

            var paginatedResult = await PaginatedList<CountryResponseDTO>
                .CreateAsync(practitioners
                .ProjectTo<CountryResponseDTO>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<CountryResponseDTO>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<LanguageResponseDto>>> RetrieveAllLanguages(int pageIndex, int pageSize)
        {
            _logger.LogInformation($"About Retrieving all Languages with pageNumber: {pageIndex} and pageSize {pageSize}");

            var practitioners = await _languageService.GetAllLanguages();

            var paginatedResult = await PaginatedList<LanguageResponseDto>
                .CreateAsync(practitioners
                .ProjectTo<LanguageResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<LanguageResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<State>>> StatesOfCountry(int pageIndex, int pageSize, string country)
        {
            var requestPayload = new StatesOfSpecifiedCountry
            {
                country = country
            };

            string citiesOfCountryUrl = ConfigSettings.ApplicationSetting.StatesofSpecifiedCountry;

            var jsonPayload = JsonConvert.SerializeObject(requestPayload);

            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " },
                { "Accept", "application/json" }
            };

            var response = await _apiCall.PostAsync(citiesOfCountryUrl, httpContent, headers);

            if (!string.IsNullOrEmpty(response))
            {
                var deserializedResult = JsonConvert.DeserializeObject<StatesOfSpecifiedCountryResponse>(response);

                var paginatedResult = await PaginatedList<State>
                .CreateAsync(deserializedResult.data.states, pageIndex, pageSize);

                return new SuccessDataResult<PaginatedList<State>>(paginatedResult);
            }
            else
            {
                return new ErrorDataResult<PaginatedList<State>>(null, StatusCode_NoCitiesFound, "No Cities Found");
            }
        }
    }
}
