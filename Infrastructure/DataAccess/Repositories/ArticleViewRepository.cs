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
    public class ArticleViewRepository : GenericRepository<ArticleView>, IArticleViewRepository
    {
        public ArticleViewRepository(HealthTriageDbContext HealthTriageDbContext) : base(HealthTriageDbContext)
        {
        }
    }
}
