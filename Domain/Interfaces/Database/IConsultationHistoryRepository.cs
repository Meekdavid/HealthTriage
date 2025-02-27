using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface IConsultationHistoryRepository : IGenericRepository<ConsultationHistory>
    {
        Task<IQueryable<ConsultationHistory>> RetrieveAllConsultationHistory();
        Task<IQueryable<ConsultationHistory>> RetrieveConsultationHistoryForSpecificDateRange(DateTime startDate, DateTime endDate);
        Task<IQueryable<ConsultationHistory>> RetrieveConsultationsForSpecificUser(string userId);
    }
}
