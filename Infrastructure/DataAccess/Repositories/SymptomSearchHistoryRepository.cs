using Domain.Interfaces.Database;
using Persistence.DBContext;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositories
{
    public class SymptomSearchHistoryRepository : GenericRepository<SymptomSearchHistory>, ISymptomSearchHistoryRepository
    {
        public SymptomSearchHistoryRepository(HealthTriageDbContext HealthTriageDbContext) : base(HealthTriageDbContext)
        {
        }

        public async Task<IQueryable<SymptomSearchHistory>> RetrieveSymptomSearchHistoryForSpecificUser(string userId)
        {
            var symptomHistories = _ctx.SymptomSearchHistories.Where(sh => sh.UserId == userId);

            return symptomHistories;
        }
    }
}
