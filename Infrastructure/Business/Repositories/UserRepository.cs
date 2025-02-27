using Common.Enums;
using Domain.Interfaces.Database;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<AppUser>> GetAllUsers()
        {
            return _ctx.Users.Where(u => (!string.IsNullOrEmpty(u.Email)));
        }

        public async Task<IQueryable<AppUser>> GetAllUsersByType(AuthorType userType)
        {
            return _ctx.Users.Where(u => u.Role == userType.ToString());
        }

        public async Task<AppUser> GetPractitionerByUserEmail(string email)
        {
            return await _ctx.Users.Where(p => p.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetPractitionerByUserId(string id)
        {
            return await _ctx.Users.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
