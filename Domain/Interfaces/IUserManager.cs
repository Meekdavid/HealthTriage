using Common.Models;
using Core.Results;
using Microsoft.AspNetCore.Http;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserManager
    {
        Task<IDataResult<Token>> CreateAccessToken(AppUser user);
        Task<IDataResult<string>> CreateUserAndAssignRolesAsync(UserRegisterRequest userRegisterRequest, List<string> roles, HttpContext httpContext);
        Task<IDataResult<Token>> SignInAsync(string email, string password);
        Task<IDataResult<string>> ResetPasswordAsync(ResetPasswordRequest req);
        Task<IDataResult<string>> GeneratePasswordResetTokenAsync(string email);
        Task<string> GeneratePasswordResetUrl(string email, string token);
        Task<IDataResult<string>> ChangeUserPasswordAsync(ChangePasswordRequest req, string userId);
        Task<IDataResult<Token>> CreateAccessToken(string userId);
        Task<Core.Results.IResult> ChangeRefreshToken(UserChangeRefreshTokenRequest request);
        Task<IDataResult<string>> GenerateEmailConfirmationTokenAsync(string email);
    }
}
