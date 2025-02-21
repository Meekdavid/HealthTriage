using Domain.Interfaces;
using Persistence.DBContext;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(HealthTriageDbContext healthTriageContext) : base(healthTriageContext)
        {
        }
    }
}
