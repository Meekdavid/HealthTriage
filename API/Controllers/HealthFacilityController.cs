using Common.Enums;
using Common.Models;
using Common.Pagination;
using Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;
using Persistence.DBModels;

namespace API.Controllers
{
    [ApiController]
    [Route("api/facility")]
    public class HealthCareFacilityController : Controller
    {
        private readonly IHealthFacilityBusiness _healthCareFacilityHelper;

        public HealthCareFacilityController(IHealthFacilityBusiness healthCareFacilityHelper)
        {
            _healthCareFacilityHelper = healthCareFacilityHelper;
        }

        /// <summary>
        /// Fetches medical facilities based on city and health amenity type.
        /// </summary>
        /// <remarks>
        /// **Endpoint:** `GET /api/medical-facilities/search`  
        ///  
        /// This endpoint allows clients to retrieve medical facilities in a specific city based on the selected health amenity type.
        ///
        /// **Valid Amenity Types:**  
        /// - `Hospital`  
        /// - `Clinic`  
        /// - `Doctors`  
        /// - `Dentist`  
        /// - `Pharmacy`  
        /// - `Veterinary`  
        /// - `NursingHome`  
        /// - `Healthcare`  
        /// - `SocialFacility`  
        /// - `AlternativeMedicine`  
        /// - `BloodDonation`  
        /// - `Chiropractor`  
        /// - `Physiotherapist`  
        ///
        /// **Example Request (cURL):**  
        /// ```sh
        /// curl -X GET "/api/facility/search
        /// -H "Accept: application/json"
        /// ```  
        ///
        /// **Response Codes:**  
        /// - **200** → Returns a paginated list of medical facilities  
        /// - **400** → Invalid amenity type provided  
        /// - **500** → Server error while processing the request  
        /// </remarks>
        /// <param name="cityName">The name of the city to search in.</param>
        /// <param name="amenity">The type of medical facility (Must be one of the valid amenity types).</param>
        /// <param name="pageIndex">The page index for pagination (Default: 1).</param>
        /// <param name="pageSize">The number of results per page (Default: 10).</param>
        /// <returns>A paginated list of medical facilities.</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<HospitalClientResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedicalFacilities(
            [FromQuery] string cityName,
            [FromQuery] HealthAmenityType amenity,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _healthCareFacilityHelper.FetchMedicalFacilitiesAsync(cityName, amenity.ToString(), pageIndex, pageSize);

            return Ok(result);
        }
    }
}
