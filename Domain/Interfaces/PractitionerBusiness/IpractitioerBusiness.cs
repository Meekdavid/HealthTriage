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

namespace Domain.Interfaces.PractitionerBusiness
{
    public interface IpractitioerBusiness
    {
        Task<Core.Results.IResult> AddPractitioner(PractitionerRequest request, HttpContext httpContext);
        Task<Core.Results.IResult> RatePractitioner(int rate, string Id);
        Task<Core.Results.IResult> DeletePractitioner(string id);
        Task<Core.Results.IResult> ApprovePractitionerApplication(string id);
        Task<IDataResult<PaginatedList<Practitioner>>> RetrieveAllPractitioners(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<Practitioner>>> RetrieveUnapprovedPractitioners(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<Practitioner>>> SearchPractitioners(int pageIndex, int pageSize);
        Task<IDataResult<Practitioner>> RetrievePractitionerById(string Id);
    }
}
