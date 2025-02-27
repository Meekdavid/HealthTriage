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
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(HealthTriageDbContext healthTriageContext) : base(healthTriageContext)
        {
        }

        public async Task<IQueryable<Language>> GetAllLanguages()
        {
            return _ctx.Languages.Where(a => !string.IsNullOrEmpty(a.ISOCode));
        }
    }
}
