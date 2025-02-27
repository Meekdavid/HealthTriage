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
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly IpractitioerBusiness _practitionerService;
        private readonly IMedicalActivityBusiness _medicalActivityService;
        private readonly IConsultationHistoryBusiness _consultationHistoryService;
        private readonly ISymptomSearchHistoryBusiness _symptomsSearchHistory;
        private readonly IUserManager _userManager;

        public AdminController(IpractitioerBusiness practitionerService,
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
        /// Deletes a practitioner.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `DELETE /api/admin/DeletePractitioner/{Practitionerid}`  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Practitioner deleted successfully  
        /// - **200** → 14 - Invalid input  
        /// - **200** → 19 - User Not Found  
        /// - **200** → 09 - Exception Occurred, Contact Developer 
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource  
        /// </remarks>
        /// <param name="Practitionerid">The Practitioner Unique Id</param>
        [HttpDelete("DeletePractitioner/{Practitionerid}")]
        [ProducesResponseType(typeof(Core.Results.IResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Core.Results.IResult>> DeletePractitioner([FromRoute] string Practitionerid)
        {
            var result = await _practitionerService.DeletePractitioner(Practitionerid);
            return Ok(result);
        }

        /// <summary>
        /// Approves a practitioner's application.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `PATCH /api/admin/approve/{Practitionerid}`  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Practitioner approved  
        /// - **200** → 14 - Invalid input  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="Practitionerid">The Practitioner Unique Id</param>
        [HttpPatch("approve/{Practitionerid}")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Core.Results.IResult>> ApprovePractitionerApplication([FromRoute] string Practitionerid)
        {
            var result = await _practitionerService.ApprovePractitionerApplication(Practitionerid);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all practitioners.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/RetrieveAllPractitioners`  
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
        /// Retrieves unapproved practitioners.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/unapprovedPractitioners`  
        ///
        /// When a user applies to become a practitioner, the application is queued for an admin to review and approve.
        /// 
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        [HttpGet("unapprovedPractitioners")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<PractitionerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> RetrieveUnapprovedPractitioners([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _practitionerService.RetrieveUnapprovedPractitioners(pageIndex, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Searches for practitioners based on a search string.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/searchPractitioner`  
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
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> SearchPractitioners([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string searchString)
        {
            var result = await _practitionerService.SearchPractitioners(pageIndex, pageSize, searchString);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific practitioner by ID.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/practitioner/{id}`  
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// /// <param name="id">The Practitioner Unique Id</param>
        [HttpGet("practitioner/{id}")]
        [ProducesResponseType(typeof(SuccessDataResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PractitionerResponseDto>>> RetrievePractitionerById([FromRoute] string id)
        {
            var result = await _practitionerService.RetrievePractitionerById(id);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all user medical activities.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/AllActivities` 
        /// 
        /// This return activities performed by all users on HealthTriage
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        [HttpGet("AllActivities")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<PractitionerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> RetrieveAllMedicalActivities([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _medicalActivityService.RetrieveAllMedicalActivities(pageIndex, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all user medical activities.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/AllActivities` 
        /// 
        /// This returns activities performed by all users on HealthTriage.
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** → Unauthorized, or Authorized User not Allowed to use this Resource  
        ///
        /// **User Type (AuthorType):**  
        /// - **0** → Patient  
        /// - **1** → Practitioner  
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="userType">The type of user to filter activities by. Use <see cref="AuthorType"/> for valid values.</param>
        [HttpGet("AllActivitiesByUser")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<PractitionerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> RetrieveMedicalActivitiesByUserType(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromRoute] AuthorType userType)
        {
            var result = await _medicalActivityService.RetrieveMedicalActivitiesByUserType(pageIndex, pageSize, userType);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all user medical activities within a specific date range.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/AllActivitiesByDate` 
        /// 
        /// This returns activities performed by all users on HealthTriage within the specified date range.
        ///
        /// **Response Codes:**  
        /// - **200** → 00 - Request successful  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** → Unauthorized, or Authorized User not Allowed to use this Resource  
        ///
        /// **Date Range:**  
        /// - The date range is provided in the request headers.  
        /// - **StartDate**: The start date of the range (inclusive). Format: YYYY-MM-DD.  
        /// - **EndDate**: The end date of the range (inclusive). Format: YYYY-MM-DD.  
        /// </remarks>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="dateRange">The date range for filtering activities. Provided in the request headers.</param>
        [HttpGet("AllActivitiesByDate")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<PractitionerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IDataResult<PaginatedList<PractitionerResponseDto>>>> RetrieveMedicalActivitiesForSpecificDateRange(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromHeader] DateRangeRequest dateRange)
        {
            var result = await _medicalActivityService.RetrieveMedicalActivitiesForSpecificDateRange(pageIndex, pageSize, dateRange.StartDate, dateRange.EndDate);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves medical activities for a specific user.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/UserActivities` 
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
        /// Retrieves all consultation history
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A paginated list of consultation history records.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/admin/allconsultations
        /// 
        /// This endpoint returns a paginated list of all consultations.
        /// </remarks>
        [HttpGet("allconsultations")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<ConsultationHistoryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveAllConsultationHistory([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(await _consultationHistoryService.RetrieveAllConsultationHistory(pageIndex, pageSize));
        }

        /// <summary>
        /// Retrieves consultation history for a specific date range.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="startDate">The start date of the range (format: YYYY-MM-DD).</param>
        /// <param name="endDate">The end date of the range (format: YYYY-MM-DD).</param>
        /// <returns>A paginated list of consultation history within the given date range.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/admin/consultationsbydate
        /// 
        /// This endpoint retrieves consultations between the specified dates.
        /// </remarks>
        [HttpGet("consultationsbydate")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<ConsultationHistoryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveConsultationHistoryForSpecificDateRange(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            return Ok(await _consultationHistoryService.RetrieveConsultationHistoryForSpecificDateRange(pageIndex, pageSize, startDate, endDate));
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
        ///     GET /api/admin/consultationbyuser/12345
        /// 
        /// This endpoint returns consultation history specific to a user.
        /// </remarks>
        [HttpGet("consultationsbyuser/{userId}")]
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
        ///     GET /api/admin/symptom-search/user/12345
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
        /// Retrieves a user profile Details by User Id.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/{userId}`  
        ///  
        /// This endpoint fetches user details using the provided user ID.
        ///
        /// **Example Request (cURL):**  
        /// ```sh
        /// curl -X GET "https://healthtriage.runasp.net/api/admin/123456" \
        /// -H "Accept: application/json"
        /// ```  
        ///
        /// **Response Codes:**  
        /// - **200** → User profile retrieved successfully  
        /// - **200** → 19 - User not found  
        /// - **500** → Server error while processing the request  
        /// </remarks>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The user profile details.</returns>
        [HttpGet("users/profile/{userId}")]
        [ProducesResponseType(typeof(IDataResult<UserProfileDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveUserById(string userId)
        {
            var result = await _userManager.RetrieveUserById(userId);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves users based on their user type.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/profile/by-type`  
        ///  
        /// This endpoint retrieves users filtered by their **UserType**.
        ///
        /// **Valid Author Types:**  
        /// - `Admin`  
        /// - `Patient`  
        /// - `Practitioner` 
        ///
        /// **Example Request (cURL):**  
        /// ```sh
        /// curl -X GET "https://healthtriage.runasp.net/api/profile/by-type" \
        /// -H "Accept: application/json"
        /// ```  
        ///
        /// **Response Codes:**  
        /// - **200** → Users retrieved successfully 
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** →  → - UnAuthorized, or Authorized User not Allowed to use this Resource
        /// </remarks>
        /// <param name="pageIndex">The page index for pagination (Default: 1).</param>
        /// <param name="pageSize">The number of results per page (Default: 10).</param>
        /// <param name="type">The user type to filter users (Must be a valid UserType).</param>
        /// <returns>A paginated list of users based on type.</returns>
        [HttpGet("by-type")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<UserProfileDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveUserByType(
            [FromQuery] AuthorType type,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
            )
        {

            var result = await _userManager.RetrieveUserByType(pageIndex, pageSize, type);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all users with pagination.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/admin/Allusers`  
        ///  
        /// This endpoint retrieves all registered users in a **paginated format**.
        ///  
        /// **Example Request (cURL):**  
        /// ```sh
        /// curl -X GET "https://healthtriage.runasp.net/api/admin/users?pageIndex=1&pageSize=10" \
        /// -H "Accept: application/json"
        /// ```  
        ///
        /// **Response Codes:**  
        /// - **200** → Users retrieved successfully  
        /// - **200** → 09 - Exception Occurred, Contact Developer  
        /// - **401** → Unauthorized or authorized user not allowed to access this resource  
        /// </remarks>
        /// <param name="pageIndex">The page index for pagination (Default: 1).</param>
        /// <param name="pageSize">The number of results per page (Default: 10).</param>
        /// <returns>A paginated list of all users.</returns>
        [HttpGet("Allusers")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<UserProfileDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveAllUsers(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10
            )
        {

            var result = await _userManager.RetrieveAllUsers(pageIndex, pageSize);

            return Ok(result);
        }

    }
}
