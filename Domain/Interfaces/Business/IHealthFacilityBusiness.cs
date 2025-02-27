using Common.Models;
using Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IHealthFacilityBusiness
    {
        Task<IDataResult<PaginatedList<HospitalClientResponseDto>>> FetchMedicalFacilitiesAsync(string cityName, string amenity, int pageIndex, int pageSize);
    }
}
