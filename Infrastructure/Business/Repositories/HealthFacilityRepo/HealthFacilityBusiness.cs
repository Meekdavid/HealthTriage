using Common.ConfigurationSettings;
using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Business.Repositories.HealthFacilityRepo
{
    public class HealthFacilityBusiness : IHealthFacilityBusiness
    {
        private readonly IApiClient _apiCall;
        public HealthFacilityBusiness(IApiClient apiCall)
        {
            _apiCall = apiCall;
        }
        public async Task<IDataResult<PaginatedList<HospitalClientResponseDto>>> FetchMedicalFacilitiesAsync(string cityName, string amenity, int pageIndex, int pageSize)
        {
            amenity = amenity.ToLower();

            var requestBody = $@"
            [out:json];
                area[name=""{cityName}""]->.searchArea;
                (
                    node[""amenity""=""{amenity}""](area.searchArea);
                    way[""amenity""=""{amenity}""](area.searchArea);
                    relation[""amenity""=""{amenity}""](area.searchArea);
                );
                    out center;
            ";

            //var content = new StringContent("data=" + Uri.EscapeDataString(requestBody), Encoding.UTF8, "application/x-www-form-urlencoded");
            var content = new StringContent("data=" + HttpUtility.UrlEncode(requestBody), Encoding.UTF8, "application/x-www-form-urlencoded");

            var facilityResponse = await _apiCall.PostAsync(ConfigSettings.ApplicationSetting.MedicalFacilityEndpoint, content);

            if (!string.IsNullOrEmpty(facilityResponse))
            {
                var desrializedResponse = JsonConvert.DeserializeObject<HospitalClientResponse>(facilityResponse);

                var paginatedResult = await PaginatedList<HospitalClientResponseDto>
                .CreateAsync(desrializedResponse.elements.Select(t => t.tags).ToList(), pageIndex, pageSize);

                return new SuccessDataResult<PaginatedList<HospitalClientResponseDto>>(paginatedResult);
            }
            else
            {
                return new ErrorDataResult<PaginatedList<HospitalClientResponseDto>>(null, StatusCode_NoFacilityFound, StatusMessage_NoFacilityFound.Replace("{Region}", cityName));
            }
        }
    }
}
