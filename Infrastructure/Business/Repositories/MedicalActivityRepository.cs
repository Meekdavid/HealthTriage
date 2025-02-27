using Common.Enums;
using Domain.Interfaces.Database;
using Infrastructure.DataAccess.Repositories;
using Persistence.DBContext;
using Persistence.DBModels;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Repositories
{
    public class MedicalActivityRepository : GenericRepository<MedicalActivityLog>, IMedicalActivityRepository
    {

        public MedicalActivityRepository(HealthTriageDbContext healthTriageContext) : base(healthTriageContext)
        {
        }

        public async Task<IQueryable<MedicalActivityLog>> RetrieveAllMedicalActivities()
        {
            var activities = _ctx.MedicalActivityLogs.Where(x => x.Status == Status.Active);
            return activities;
        }

        public async Task<IQueryable<MedicalActivityLog>> RetrieveMedicalActivitiesByUserType(AuthorType userType)
        {
            var activities = _ctx.MedicalActivityLogs.Where(x => x.Status == Status.Active && x.UserType == userType);
            return activities;
        }

        public async Task<IQueryable<MedicalActivityLog>> RetrieveMedicalActivitiesForSpecificDateRange(DateTime startDate, DateTime endDate)
        {
            var activities = _ctx.MedicalActivityLogs
                .Where(x => x.Status == Status.Active && x.CreatedDate >= startDate && x.CreatedDate <= endDate);

            return activities;
        }

        public async Task<IQueryable<MedicalActivityLog>> RetrieveMedicalActivitiesForSpecificUser(string userId)
        {
            var activities = _ctx.MedicalActivityLogs.Where(x => x.Status == Status.Active && x.UserId == userId);
            return activities;
        }
    }
}
