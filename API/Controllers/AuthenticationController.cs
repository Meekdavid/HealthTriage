using Common.Models;
using Core.Results;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// Authenticates the user and generates a new access token.
        /// </summary>
        /// <remarks>
        /// If the login is successful, a new token is generated, and the user's refresh token is updated.
        /// <response code="200">00 - Request Successful</response>
        /// <response code="400">14 - Wrong Input Supplied</response>
        /// <response code="401">18 - Login Failed</response>
        /// <response code="403">17 - Email Address Not Confirmed</response>
        /// <response code="500">09 - Exception Occurred, Contact Developer</response>
        /// </remarks>
        /// <param name="userModel">The login request containing user credentials.</param>
        /// <returns>Returns a response with the authentication token.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
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
        /// Registers a new user on HealthTriage.
        /// </summary>
        /// <remarks>
        /// The client must send user registration details in JSON format.
        /// Example request (cURL):
        /// ```sh
        /// curl -X POST "http://localhost:yourport/api/user/register" \
        /// -H "Content-Type: application/json" \
        /// -d '{
        ///     "username": "john_doe",
        ///     "email": "john.doe@example.com",
        ///     "password": "securePassword123"
        /// }'
        /// ```
        ///
        /// Example request (JavaScript Fetch API):
        /// ```javascript
        /// const userData = {
        ///     username: "john_doe",
        ///     email: "john.doe@example.com",
        ///     password: "securePassword123"
        /// };
        ///
        /// fetch("http://localhost:yourport/api/user/register", {
        ///     method: "POST",
        ///     headers: {
        ///         "Content-Type": "application/json"
        ///     },
        ///     body: JSON.stringify(userData)
        /// }).then(response => response.json())
        ///   .then(data => console.log(data))
        ///   .catch(error => console.error("Error:", error));
        /// ```
        /// <response code="200">00 - User registered successfully</response>
        /// <response code="400">14 - Invalid input (e.g., missing fields or invalid email)</response>
        /// <response code="409">16 - User already exists (e.g., duplicate username or email)</response>
        /// <response code="500">09 - Server error</response>
        /// </remarks>
        /// <param name="newUser">The user registration details (username, email, password).</param>
        /// <returns>Returns a response indicating the registration status.</returns>
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
        /// Sends a password reset token to the user's email.
        /// </summary>
        /// <remarks>
        /// Generates a password reset token and emails it to the user.
        /// <response code="200">00 - Request Successful</response>
        /// <response code="400">14 - Wrong Input Supplied</response>
        /// <response code="404">19 - User Not Found</response>
        /// <response code="500">09 - Exception Occurred, Contact Developer</response>
        /// </remarks>
        /// <param name="model">The request containing the user's email.</param>
        /// <returns>Returns a response indicating whether the request was successful.</returns>
        [HttpPost("forgot-password")]
        [Consumes("application/json")]
        [Produces("application/json")]
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
        /// Changes the user's password using a valid reset token.
        /// <response code="200">00 - Request Successful</response>
        /// <response code="400">14 - Wrong Input Supplied</response>
        /// <response code="404">19 - User Not Found</response>
        /// <response code="422">20 - Password Reset Failed</response>
        /// <response code="500">09 - Exception Occurred, Contact Developer</response>
        /// </remarks>
        /// <param name="model">The password reset request containing the reset token and new password.</param>
        /// <returns>Returns a response indicating whether the password reset was successful.</returns>
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
        /// Requires authentication. The user must provide the current password and a new password.
        /// <response code="200">00 - Request Successful</response>
        /// <response code="400">14 - Wrong Input Supplied</response>
        /// <response code="401">19 - User Not Found</response>
        /// <response code="403">21 - Wrong Password</response>
        /// <response code="422">22 - Unable to Remove Password for User</response>
        /// <response code="422">23 - Unable to Add New Password for User</response>
        /// <response code="500">09 - Exception Occurred, Contact Developer</response>
        /// </remarks>
        /// <param name="model">The change password request containing the old and new password.</param>
        /// <returns>Returns a response indicating whether the password change was successful.</returns>
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
    }
}