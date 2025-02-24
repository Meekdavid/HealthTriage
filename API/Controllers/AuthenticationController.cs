using Common.ConfigurationSettings;
using Common.DTOs;
using Common.Models;
using Core.Results;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUserContextManager _userContextService;
        public AuthenticationController(IUserManager userManager, IUserContextManager userContextService)
        {
            _userManager = userManager;
            _userContextService = userContextService;
        }        

        /// <summary>
        /// Registers a new user on HealthTriage.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `POST /api/auth/register`  
        ///  
        /// The client must send user registration details as `multipart/form-data`.  
        ///
        /// **Example Request (cURL):**  
        /// ```sh
        /// curl -X POST "http://localhost:yourport/api/auth/register" \
        /// -H "Content-Type: multipart/form-data" \
        /// -F "username=john_doe" \
        /// -F "email=john.doe@example.com" \
        /// -F "password=securePassword123"
        /// ```  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - User registered successfully  
        /// - **200** → 14 - Invalid input (e.g., missing fields or invalid email)  
        /// - **200** → 16 - User already exists (e.g., duplicate username or email)  
        /// - **200** → 09 - Exception Occurred, Contact Developer 
        /// </remarks>
        [HttpPost("register")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<IDataResult<string>>> Register([FromForm] UserRegisterRequest newUser)
        {
            var result = await _userManager.CreateUserAndAssignRolesAsync(newUser, new List<string> { "Patient" }, HttpContext);
            return Ok(result);
        }

        /// <summary>
        /// Authenticates the user and generates a new access token.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `POST /api/auth/login`  
        ///  
        /// If the login is successful, a new token is generated, and the user's refresh token is updated.  
        ///
        /// **Responses Codes:**  
        /// - **200** → 00 - Request Successful  
        /// - **200** → 14 - Wrong Input Supplied  
        /// - **200** → 18 - Login Failed  
        /// - **200** → 17 - Email Address Not Confirmed  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessDataResult<SuccessfulLoginDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<Token>>> Login([FromBody] UserLoginRequest userModel)
        {
            var result = await _userManager.SignInAsync(userModel.Email, userModel.Password);
            return Ok(result);
        }


        /// <summary>
        /// Sends a password reset token to the user's email.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `POST /api/auth/forgot-password`  
        ///
        /// Generates a password reset token and emails it to the user.  
        ///
        /// **Responses Codes:**  
        /// - **200** → 00 - Request Successful  
        /// - **200** → 14 - Wrong Input Supplied  
        /// - **200** → 19 - User Not Found  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// </remarks>
        [HttpPost("forgot-password")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Core.Results.IResult>> RequestPasswordReset([FromBody] GeneratePasswordResetTokenRequest model)
        {
            var result = await _userManager.GeneratePasswordResetTokenAsync(model.Email);
            return Ok(result);
        }

        /// <summary>
        /// Resets the user's password using the provided reset token.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `POST /api/auth/reset-password`  
        ///
        /// Changes the user's password using a valid reset token.  
        ///
        /// **Responses Codes:**  
        /// - **200** → 00 - Request Successful  
        /// - **200** → 14 - Wrong Input Supplied  
        /// - **200** → 19 - User Not Found  
        /// - **200** → 20 - Password Reset Failed  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// </remarks>
        [HttpPost("reset-password")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Core.Results.IResult>> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var result = await _userManager.ResetPasswordAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `POST /api/auth/change-password`  
        ///
        /// Requires authentication. The user must provide the current password and a new password.  
        ///
        /// **Responses Codes:**  
        /// - **200** → 00 - Request Successful  
        /// - **200** → 14 - Wrong Input Supplied  
        /// - **200** → 19 - User Not Found  
        /// - **200** → 21 - Wrong Password  
        /// - **200** → 22 - Unable to Remove Password for User  
        /// - **200** → 23 - Unable to Add New Password for User  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// </remarks>
        [Authorize]
        [HttpPost("change-password")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Core.Results.IResult>> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            string userId = await _userContextService.GetUserId();
            var result = await _userManager.ChangeUserPasswordAsync(model, userId);
            return Ok(result);
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <remarks>The endpoint is consumed from backend. The endpoint Confirms user's email with token which has been sent. If success, displays success page and provide a button for user to login. The endpoint is fired when a user clicks on <b>Confirm Email</b> in their mailbox</remarks>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var confirmationResult = await _userManager.ConfirmEmailAsync(email, token);
            string healthTriageHomePage = ConfigSettings.ApplicationSetting.HealthTriageHomePage;
            bool success = StatusCode_Success == "00" ? true : false;

            // Redirect to the static HTML page and pass confirmation result as query params
            return Redirect($"/confirm-email.html?success={success}&message={Uri.EscapeDataString(confirmationResult.ResponseDescription)}&email={Uri.EscapeDataString(email)}");
        }

        /// <summary>
        /// Retrieve HealthTriage Homepage
        /// </summary>
        /// <remarks>The endpoint is consumed from backend</remarks>
        /// <param name="message"></param>
        /// <param name="success"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("get-confirmation-page")]
        public IActionResult GetConfirmationPage(string email, bool success, string message)
        {
            string healthTriageHomePage = ConfigSettings.ApplicationSetting.HealthTriageHomePage;

            return Ok(new
            {
                success,
                message,
                email,
                page = healthTriageHomePage
            });
        }

        /// <summary>
        /// Resend Email Confirmation Mail
        /// </summary>
        /// <remarks>The endpoint is consumed from backend</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmation([FromBody] ResendConfirmationRequest request)
        {
            return Ok(await _userManager.ResendConfirmationEmail(request.Email));
        }

    }
}
