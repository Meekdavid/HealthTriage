using Domain.Interfaces.Database;
using Persistence.DBContext;
using Persistence.DBModels;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositories
{
    public class ConsultationHistoryRepository : GenericRepository<ConsultationHistory>, IConsultationHistoryRepository
    {
        public ConsultationHistoryRepository(HealthTriageDbContext HealthTriageDbContext) : base(HealthTriageDbContext)
        {
        }

        public async Task<IQueryable<ConsultationHistory>> RetrieveAllConsultationHistory()
        {
            var consultations = _ctx.ConsultationHistories.Where(ss => ss.Status == Status.Active);
            return consultations;
        }

        public async Task<IQueryable<ConsultationHistory>> RetrieveConsultationHistoryForSpecificDateRange(DateTime startDate, DateTime endDate)
        {
            var consultations = _ctx.ConsultationHistories.Where(ss => ss.Status == Status.Active && (ss.CreatedDate >= startDate && ss.CreatedDate <= endDate));
            return consultations;
        }

        public async Task<IQueryable<ConsultationHistory>> RetrieveConsultationsForSpecificUser(string userId)
        {
            var consultations = _ctx.ConsultationHistories.Where(ss => ss.Status == Status.Active && ss.UserId == userId);
            return consultations;
        }
    }
}
