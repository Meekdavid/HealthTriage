using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Domain.Interfaces.Business;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Repositories.FAQRepo
{
    public class FAQBusiness : IFAQBusiness
    {
        public Task<Core.Results.IResult> AddFAQ(FAQRequest request, HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Results.IResult> DeleteFAQ(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PaginatedList<FAQResponseDto>>> RetrieveAllFAQ(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<PaginatedList<FAQResponseDto>>> SearchFAQ(int pageIndex, int pageSize, string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
