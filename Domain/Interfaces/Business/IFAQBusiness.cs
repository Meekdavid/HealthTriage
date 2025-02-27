using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IFAQBusiness
    {
        Task<Core.Results.IResult> AddFAQ(FAQRequest request, HttpContext httpContext);
        Task<Core.Results.IResult> DeleteFAQ(string id);
        Task<IDataResult<PaginatedList<FAQResponseDto>>> RetrieveAllFAQ(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<FAQResponseDto>>> SearchFAQ(int pageIndex, int pageSize, string searchString);
    }
}
