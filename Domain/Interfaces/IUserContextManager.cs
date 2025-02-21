using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserContextManager
    {
        Task<string> GetUserId();
        Task<List<string>> GetUserRoles();
        Task<bool> IsInRole(string role);
        Task<string> GetUserName();
        Task<string> GetUserEmail();
        Task<List<Claim>> GetUserClaims();
    }
}
