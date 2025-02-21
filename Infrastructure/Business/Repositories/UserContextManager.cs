using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Repositories
{
    public class UserContextManager : IUserContextManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Claim>> GetUserClaims()
        {
            return _httpContextAccessor.HttpContext.User.Claims.ToList();
        }

        public async Task<string> GetUserEmail()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            return email;
        }

        public async Task<string> GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public async Task<string> GetUserName()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return userName;
        }

        public async Task<List<string>> GetUserRoles()
        {
            return _httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        }

        public async Task<bool> IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }
    }
}
