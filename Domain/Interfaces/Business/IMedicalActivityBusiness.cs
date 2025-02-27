using Common.DTOs;
using Common.Enums;
using Common.Models;
using Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IMedicalActivityBusiness
    {
        Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveAllMedicalActivities(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveMedicalActivitiesByUserType(int pageIndex, int pageSize, AuthorType userType);
        Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveMedicalActivitiesForSpecificDateRange(int pageIndex, int pageSize, DateTime startDate, DateTime endDate);
        Task<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>> RetrieveMedicalActivitiesForSpecificUser(int pageIndex, int pageSize, string userId);
    }
}
