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
    public interface ISymptomSearchHistoryBusiness
    {
        Task<IDataResult<PaginatedList<SymptomSearchHistoryResponseDto>>> RetrieveSymptomSearchHistoryForSpecificUser(int pageIndex, int pageSize, string userId);
    }
}
