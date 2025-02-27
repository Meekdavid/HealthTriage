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
    public class FAQRepository : GenericRepository<FAQ>, IFAQRepository
    {
        public FAQRepository(HealthTriageDbContext HealthTriageDbContext) : base(HealthTriageDbContext)
        {
        }

        public async Task<IQueryable<FAQ>> GetAllFAQs()
        {
            var faqs = _ctx.FAQs.Where(f => f.Status == Status.Active);

            return faqs;
        }

        public async Task<IQueryable<FAQ>> SearchForFAQs(string searchString)
        {
            var faqs = _ctx.FAQs.Where(f => f.Status == Status.Active && (f.Question.Contains(searchString) || f.Answer.Contains(searchString)));

            return faqs;
        }
    }
}
