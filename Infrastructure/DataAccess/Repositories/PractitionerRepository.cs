using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public class PractitionerRepository : GenericRepository<Practitioner>, IPractitionerRepository
    {

        public PractitionerRepository(HealthTriageDbContext healthTriageContext) : base(healthTriageContext)
        {
        }

        public async Task<IQueryable<Practitioner>> GetAllPractitioners()
        {
            return _ctx.Practitioners.Where(p => p.Status == Status.Active)
                .Include(u => u.User);
        }

        public async Task<Practitioner> GetPractitionerByPractitionerId(string id)
        {
            return await _ctx.Practitioners.Where(p => p.PractitionerId == id)
                .Include(u => u.User)
                .FirstOrDefaultAsync();
        }

        public async Task<Practitioner> GetPractitionerByUserId(string id)
        {
            return await _ctx.Practitioners
                .Include(u => u.User)
                .Where(user => user.UserId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Practitioner>> GetUnapprovedPractitioners()
        {
            return _ctx.Practitioners.Where(p => p.Status == Status.Passive)
                .Include(u => u.User);
        }

        public async Task<IQueryable<Practitioner>> SearchForPractitioners(string searchString)
        {
            searchString = searchString.ToLower();

            return _ctx.Practitioners
                .Include(u => u.User)
                .Where(user => user.UserId.Contains(searchString)
                || user.User.FullName.Contains(searchString)
                || user.User.UserName.Contains(searchString)
                || user.PractitionerTitle.Contains(searchString)
                || user.PractitionerName.Contains(searchString)
                || user.Institution.Contains(searchString));
        }
    }
}
