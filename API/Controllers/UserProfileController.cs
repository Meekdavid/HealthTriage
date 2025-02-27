using Common.DTOs;
using Common.Enums;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces;
using Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class UserProfileController : Controller
    {
        private readonly IpractitioerBusiness _practitionerService;
        private readonly IMedicalActivityBusiness _medicalActivityService;
        private readonly IConsultationHistoryBusiness _consultationHistoryService;
        private readonly ISymptomSearchHistoryBusiness _symptomsSearchHistory;
        private readonly IUserManager _userManager;

        public UserProfileController(IpractitioerBusiness practitionerService,
            IMedicalActivityBusiness medicalActivityService,
            IConsultationHistoryBusiness consultationHistoryService,
            ISymptomSearchHistoryBusiness symptomsSearchHistory,
            IUserManager userManager)
        {
            _practitionerService = practitionerService;
            _medicalActivityService = medicalActivityService;
            _consultationHistoryService = consultationHistoryService;
            _symptomsSearchHistory = symptomsSearchHistory;
            _userManager = userManager;
        }

        /// <summary>
        /// Rates a practitioner.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `PATCH /api/profile/ratePractitioner/{id}`  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Rating submitted successfully  
        /// - **200** → 14 - Invalid input  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="rate">Rating of a practitioner between 1 and 5</param>
        /// <param name="id">The Practitioner Unique Id</param>
        [HttpPatch("ratePractitioner/{id}")]
        [ProducesResponseType(typeof(Core.Results.IResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Core.Results.IResult>> RatePractitioner([FromQuery] int rate, [FromRoute] string id)
        {
            var result = await _practitionerService.RatePractitioner(rate, id);
            return Ok(result);
        }

        /// <summary>
        /// Searches for practitioners based on a search string.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/profile/searchPractitioner`  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="searchString">Any detail about the practitioner, that would return matching records based on this detail</param>
        [HttpGet("searchPractitioner")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<PractitionerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> SearchPractitioners([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string searchString)
        {
            var result = await _practitionerService.SearchPractitioners(pageIndex, pageSize, searchString);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all practitioners.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/profile/RetrieveAllPractitioners`  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        [HttpGet("RetrieveAllPractitioners")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<PractitionerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> RetrieveAllPractitioners([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _practitionerService.RetrieveAllPractitioners(pageIndex, pageSize);
            return Ok(result);
        }


        /// <summary>
        /// Retrieves medical activities for a specific user.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/profile/UserActivities` 
        /// 
        /// This returns activities performed by a specific user on HealthTriage.
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** → Unauthorized, or Authorized User not Allowed to use this Resource  
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="userId">The unique identifier of the user to filter activities by.</param>
        [HttpGet("UserActivities")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<MedicalActivityLogResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<MedicalActivityLogResponseDto>>>> RetrieveMedicalActivitiesForSpecificUser(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] string userId)
        {
            var result = await _medicalActivityService.RetrieveMedicalActivitiesForSpecificUser(pageIndex, pageSize, userId);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves consultation history for a specific user.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A paginated list of consultation history for the specified user.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/profile/consultationbyuser/12345
        /// 
        /// This endpoint returns consultation history specific to a user.
        /// </remarks>
        [HttpGet("consultationbyuser/{userId}")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<ConsultationHistoryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveConsultationsForSpecificUser(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromRoute] string userId)
        {
            var result = await _consultationHistoryService.RetrieveConsultationsForSpecificUser(pageIndex, pageSize, userId);

            return Ok(result);
        }


        /// <summary>
        /// Retrieves symptom search history for a specific user.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A paginated list of symptom search history for the specified user.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/profile/symptom-search/user/12345
        /// 
        /// This endpoint returns the symptom search history specific to a user.
        /// </remarks>
        [HttpGet("symptom-search/user/{userId}")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<SymptomSearchHistoryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveSymptomSearchHistoryForSpecificUser(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromRoute] string userId)
        {
            var result = await _symptomsSearchHistory.RetrieveSymptomSearchHistoryForSpecificUser(pageIndex, pageSize, userId);

            return Ok(result);
        }


        /// <summary>
        /// Retrieves a user profile Details by email.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/profile/users{userId}`  
        ///  
        /// This endpoint fetches user details using the provided user email.
        ///
        /// **Example Request (cURL):**  
        /// ```sh
        /// curl -X GET "https://healthtriage.runasp.net/api/profile/users/123456" \
        /// -H "Accept: application/json"
        /// ```  
        ///
        /// **Response Codes:**  
        /// - **200** → User profile retrieved successfully  
        /// - **200** → 19 - User not found  
        /// - **500** → Server error while processing the request  
        /// </remarks>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user profile details.</returns>
        [HttpGet("users/{email}")]
        [ProducesResponseType(typeof(IDataResult<UserProfileDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveUserById(string email)
        {
            var result = await _userManager.RetrieveUserById(email);

            return Ok(result);
        }

    }
}
