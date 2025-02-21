using AutoMapper;
using Common.AutoMapperProf;
using Common.ConfigurationSettings;
using Common.Models;
using Core.Results;
using Domain.Interfaces;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Business.Repositories
{
    public class AppUserManager : IUserManager
    {
        private UserManager<AppUser> _userManager { get; set; }
        private readonly ILogger<AppUserManager> _logger;
        private readonly TokenHandler _tokenHandler;
        private readonly IEmailServiceCustom _emailHandler;
        private readonly IUserRepository _userDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILocalStorage _storage;
        private readonly int _refreshTokenExpiryDays = ConfigSettings.ApplicationSetting.RefreshTokenExpiryDays;
        public AppUserManager(UserManager<AppUser> userManager, ILogger<AppUserManager> logger, TokenHandler tokenHandler, IUserRepository userDal, IUnitOfWork unitOfWork, IMapper mapper, ILocalStorage storage, IEmailServiceCustom emailHandler)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _userDal = userDal;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
            _emailHandler = emailHandler;
        }

        public async Task<IDataResult<Token>> CreateAccessToken(AppUser user)
        {
            _logger.LogInformation($"Creating access token for UserId: {user.Id}");

            var rolesResult = await _userManager.GetRolesAsync(user);
            Token token = await _tokenHandler.CreateAccessTokenAsync(user, rolesResult.ToList());

            var changeRefreshTokenRequest = new UserChangeRefreshTokenRequest
            {
                UserId = user.Id,
                RefreshToken = token.RefreshToken,
                RefreshTokenEndDate = token.Expiration.AddDays(_refreshTokenExpiryDays)
            };

            this.ChangeRefreshToken(changeRefreshTokenRequest);

            _userDal.Update(user);
            _unitOfWork.SaveChanges();

            return new SuccessDataResult<Token>(token);
        }

        public async Task<IDataResult<string>> CreateUserAndAssignRolesAsync(UserRegisterRequest userRegisterRequest, List<string> roles, HttpContext httpContext)
        {
            //var user = _mapper.Map<AppUser>(userRegisterRequest);
            //user.LastActive = DateTime.UtcNow;
            //user.UserName = userRegisterRequest.Nickname;
            //user.Password = userRegisterRequest.Password;
            string pathNewName = $"Uploads/User/{userRegisterRequest.Nickname}";
            var profilePicture = await _storage.SingleUploadAsync(pathNewName, userRegisterRequest.ProfilePicture, httpContext);

            var user = new AppUser
            {
                FullName = userRegisterRequest.FullName,
                DOB = userRegisterRequest.DOB,
                Gender = userRegisterRequest.Gender,
                Email = userRegisterRequest.Email,
                PhoneNumber = userRegisterRequest.Phone,
                Address = userRegisterRequest.Address,
                ZipCode = userRegisterRequest.ZipCode,
                UserName = userRegisterRequest.Nickname,
                BloodGroup = userRegisterRequest.BloodGroup,
                Height = userRegisterRequest.Height,
                Weight = userRegisterRequest.Weight,
                EmergencyContact = userRegisterRequest.EmergencyContact,
                LastActive = DateTime.UtcNow,
                ProfilePicture = profilePicture.Data.pathOrContainerName,
                Id = Ulid.NewUlid().ToString(),
                EmailConfirmed = true
            };

            await _unitOfWork.BeginTransactionAsync();
            _logger.LogInformation($"Transaction started for creating user with Email: {user.Email}");

            var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User created successfully with Email: {user.Email}. Assigning roles...");

                foreach (var role in roles)
                {
                    var res = await _userManager.AddToRoleAsync(user, role);
                    if (!res.Succeeded)
                    {
                        _logger.LogInformation($"Failed to assign role '{role}' to user with Email: {user.Email}");
                        _unitOfWork.Rollback();
                        return new ErrorDataResult<string>(StatusCode_RoleAssignmentFailed, StatusMessage_RoleAssignmentFailed);
                    }
                    _logger.LogInformation($"Role '{role}' assigned successfully to user with Email: {user.Email}");
                }

                if (user.EmailConfirmed)
                {
                    // Define the path to the text file in the root of the project
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WelcomeEmail.txt");

                    // Read the content of the text file
                    string fileContent = File.ReadAllText(filePath);

                    // Replace occurrences of a specific string (e.g., "oldString" with "newString")
                    string replacedContent = fileContent.Replace("[User Name]", user.FullName)
                        .Replace("[Website Link]", ConfigSettings.ApplicationSetting.HealthTriageHomePage)
                        .Replace("[Dashboard Link]", ConfigSettings.ApplicationSetting.HealthTriageHomePage)
                        .Replace("[Unsubscribe Link]", ConfigSettings.ApplicationSetting.HealthTriageUnsubscribeLink);

                    await _emailHandler.SendEmailAsync(user.Email, "Welcome to HealthTriage", replacedContent);
                    _logger.LogInformation($"Welcome email sent to user with Email: {user.Email}");
                }
                else
                {
                    var confirmationTokenResult = await this.GenerateEmailConfirmationTokenAsync(user.Email);
                    _logger.LogInformation($"Email confirmation token generated for user with Email: {user.Email}");

                    await _emailHandler.SendConfirmationEmail(user.Email, confirmationTokenResult.Data);
                    _logger.LogInformation($"Confirmation email sent to user with Email: {user.Email}");
                }                

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                _logger.LogInformation($"Transaction committed and changes saved for user with Email: {user.Email}");

                return new SuccessDataResult<string>(user.Id);
            }
            else
            {
                _logger.LogInformation($"User creation failed for Email: {user.Email}");
                _unitOfWork.Rollback();
                return new ErrorDataResult<string>(JsonConvert.SerializeObject(result.Errors), StatusCode_UserCreationFailed, StatusMessage_UserCreationFailed);
            }
        }

        public async Task<IDataResult<Token>> SignInAsync(string email, string password)
        {
            _logger.LogInformation($"Attempting sign-in for Email: {email}");

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                if (!user.EmailConfirmed)
                {
                    _logger.LogInformation($"Email not confirmed for UserId: {user.Id}");
                    return new ErrorDataResult<Token>(null, StatusCode_UserEmailNotConfirmed, StatusMessage_UserEmailNotConfirmed);
                }

                var tokenResult = await this.CreateAccessToken(user);
                _logger.LogInformation($"Sign-in successful, token created for UserId: {user.Id}");

                return tokenResult;
            }

            _logger.LogInformation($"Login failed for Email: {email}");
            return new ErrorDataResult<Token>(null, StatusCode_LoginFailed, StatusMessage_UserEmailNotConfirmed);
        }

        public async Task<IDataResult<string>> ResetPasswordAsync(ResetPasswordRequest req)
        {
            _logger.LogInformation($"Attempting to reset password for: {req.Email}");

            var user = await _userManager.FindByEmailAsync(req.Email);

            if (user == null)
            {
                _logger.LogInformation($"User not found for email: {req.Email}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            var result = await _userManager.ResetPasswordAsync(user, req.Token, req.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Password reset successfully for: {req.Email}");
                return new SuccessDataResult<string>("");
            }

            _logger.LogInformation($"Password reset failed for: {req.Email}");
            return new ErrorDataResult<string>("", StatusCode_PasswordResetFailed, StatusMessage_PasswordResetFailed);
        }

        public async Task<IDataResult<string>> GeneratePasswordResetTokenAsync(string email)
        {
            _logger.LogInformation($"Attempting to generate password reset token for: {email}");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogInformation($"User not found for email: {email}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = await GeneratePasswordResetUrl(email, token);

            _emailHandler.SendPasswordResetToken(email, callbackUrl);
            _logger.LogInformation($"Password reset token sent to email: {email}");

            return new SuccessDataResult<string>("");
        }

        public async Task<string> GeneratePasswordResetUrl(string email, string token)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = ConfigSettings.ApplicationSetting.BaseLocalStorageDomain.Replace("https://", ""),
                Path = $"reset-password",
                Query = $"email={email}&token={token}"
            };

            var url = uriBuilder.ToString();
            _logger.LogInformation($"Generated password reset URL: {url}");

            return url;
        }

        public async Task<IDataResult<string>> ChangeUserPasswordAsync(ChangePasswordRequest req, string userId)
        {
            _logger.LogInformation($"Changing password for user ID: {userId}");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogInformation($"User not found with ID: {userId}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            var res = await _userManager.CheckPasswordAsync(user, req.Password);

            if (!res)
            {
                _logger.LogInformation($"Incorrect password for user ID: {userId}");
                return new ErrorDataResult<string>("", StatusCode_WrongPassword, StatusMessage_WrongPassword);
            }

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);

            if (!removePasswordResult.Succeeded)
            {
                _logger.LogInformation($"Failed to remove password for user ID: {userId}");
                return new ErrorDataResult<string>("", StatusCode_UnableToRemovePassword, StatusMessage_UnableToRemovePassword);
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, req.NewPassword);

            if (!addPasswordResult.Succeeded)
            {
                _logger.LogInformation($"Failed to add new password for user ID: {userId}");
                return new ErrorDataResult<string>("", StatusCode_FailedToAddNewPassword, StatusMessage_FailedToAddNewPassword);
            }

            _logger.LogInformation($"Password changed successfully for user ID: {userId}");
            return new SuccessDataResult<string>("");
        }

        public async Task<IDataResult<Token>> CreateAccessToken(string userId)
        {
            _logger.LogInformation($"Creating access token for UserId: {userId}");

            AppUser user = await _userDal.GetById(userId);
            if (user == null)
            {
                _logger.LogInformation($"User not found for UserId: {userId}");
                return new ErrorDataResult<Token>(null, StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            var rolesResult = await _userManager.GetRolesAsync(user);
            Token token = await _tokenHandler.CreateAccessTokenAsync(user, rolesResult.ToList());

            var changeRefreshTokenRequest = new UserChangeRefreshTokenRequest
            {
                UserId = user.Id,
                RefreshToken = token.RefreshToken,
                RefreshTokenEndDate = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays)
            };

            this.ChangeRefreshToken(changeRefreshTokenRequest);
            return new SuccessDataResult<Token>(token);
        }

        public async Task<Core.Results.IResult> ChangeRefreshToken(UserChangeRefreshTokenRequest request)
        {
            _logger.LogInformation($"Changing refresh token for UserId: {request.UserId}");

            var user = await _userDal.GetById(request.UserId);
            if (user == null)
            {
                _logger.LogInformation($"User not found for UserId: {request.UserId}");
                return new ErrorDataResult<Token>(null, StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            user.RefreshToken = request.RefreshToken;
            user.RefreshTokenEndDate = request.RefreshTokenEndDate;

            _userDal.Update(user);
            _unitOfWork.SaveChanges();

            _logger.LogInformation($"Refresh token updated successfully for UserId: {request.UserId}");

            return new SuccessResult("Refresh token updated successfully");
        }

        public async Task<IDataResult<string>> GenerateEmailConfirmationTokenAsync(string email)
        {
            _logger.LogInformation($"Generating email confirmation token for: {email}");

            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"User not found for email: {email}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            if (user.EmailConfirmed)
            {
                _logger.LogInformation($"Email already confirmed for: {email}");
                return new ErrorDataResult<string>("", StatusCode_UserEmailAlreadyConfirmed, StatusMessage_UserEmailAlreadyConfirmed);
            }

            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _logger.LogInformation($"Confirmation token generated for email: {email}");

            return new SuccessDataResult<string>(data: confirmationToken);
        }
    }
}
