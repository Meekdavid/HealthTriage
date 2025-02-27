using Common.Enums;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<AppUser> GetPractitionerByUserId(string id);
        Task<AppUser> GetPractitionerByUserEmail(string email);
        Task<IQueryable<AppUser>> GetAllUsers();
        Task<IQueryable<AppUser>> GetAllUsersByType(AuthorType userType);
    }
}
