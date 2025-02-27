using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IpractitioerBusiness
    {
        Task<Core.Results.IResult> AddPractitioner(PractitionerRequest request, HttpContext httpContext);
        Task<Core.Results.IResult> RatePractitioner(int rate, string Id);
        Task<Core.Results.IResult> DeletePractitioner(string id);
        Task<Core.Results.IResult> ApprovePractitionerApplication(string id);
        Task<IDataResult<PaginatedList<PractitionerResponseDto>>> RetrieveAllPractitioners(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<PractitionerResponseDto>>> RetrieveUnapprovedPractitioners(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<PractitionerResponseDto>>> SearchPractitioners(int pageIndex, int pageSize, string searchString);
        Task<IDataResult<PractitionerResponseDto>> RetrievePractitionerById(string Id);
    }
}
