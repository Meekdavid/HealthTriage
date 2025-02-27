using Common.Enums;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface IMedicalActivityRepository : IGenericRepository<MedicalActivityLog>
    {
        Task<IQueryable<MedicalActivityLog>> RetrieveAllMedicalActivities();
        Task<IQueryable<MedicalActivityLog>> RetrieveMedicalActivitiesByUserType(AuthorType userType);
        Task<IQueryable<MedicalActivityLog>> RetrieveMedicalActivitiesForSpecificDateRange(DateTime startDate, DateTime endDate);
        Task<IQueryable<MedicalActivityLog>> RetrieveMedicalActivitiesForSpecificUser(string userId);
    }
}
