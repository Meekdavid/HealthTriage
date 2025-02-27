using Common.DTOs;
using Common.Enums;
using Common.Models;
using Common.Pagination;
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
        Task<IDataResult<string>> GenerateEmailConfirmationTokenAsync(AppUser email);
        Task<IDataResult<string>> ConfirmEmailAsync(string email, string token);
        Task<IDataResult<string>> ResendConfirmationEmail(string email);
        Task<IDataResult<UserProfileDTO>> RetrieveUserById(string Id);
        Task<IDataResult<PaginatedList<UserProfileDTO>>> RetrieveUserByType(int pageIndex, int pageSize, AuthorType type);
        Task<IDataResult<PaginatedList<UserProfileDTO>>> RetrieveAllUsers(int pageIndex, int pageSize);
    }
}
