using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.DBModels;
using Common.Models;
using Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces.Database
{
    public interface IPractitionerRepository : IGenericRepository<Practitioner>
    {
        Task<IQueryable<Practitioner>> GetAllPractitioners();
        Task<IQueryable<Practitioner>> GetUnapprovedPractitioners();
        Task<IQueryable<Practitioner>> SearchForPractitioners(string searchString);
        Task<Practitioner> GetPractitionerByPractitionerId(string id);
        Task<Practitioner> GetPractitionerByUserId(string id);        

    }
}
