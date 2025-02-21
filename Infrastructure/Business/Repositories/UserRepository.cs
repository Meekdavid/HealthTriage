using Domain.Interfaces;
using Infrastructure.DataAccess.Repositories;
using Persistence.DBContext;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(HealthTriageDbContext healthTriageContext) : base(healthTriageContext)
        {
            
        }
    }
}
