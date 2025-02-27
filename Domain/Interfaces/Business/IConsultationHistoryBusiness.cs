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
    public interface IConsultationHistoryBusiness
    {
        Task<IDataResult<PaginatedList<ConsultationHistoryResponseDto>>> RetrieveAllConsultationHistory(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<ConsultationHistoryResponseDto>>> RetrieveConsultationHistoryForSpecificDateRange(int pageIndex, int pageSize, DateTime startDate, DateTime endDate);
        Task<IDataResult<PaginatedList<ConsultationHistoryResponseDto>>> RetrieveConsultationsForSpecificUser(int pageIndex, int pageSize, string userId);
    }
}
