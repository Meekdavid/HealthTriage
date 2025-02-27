using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/dropdown")]
    public class DropDownController : Controller
    {
        private readonly IDropdownBusiness _dropdownService;
        public DropDownController(IDropdownBusiness dropdownService)
        {
            _dropdownService = dropdownService;
        }

        /// <summary>
        /// Retrieves all countries with pagination.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A paginated list of countries.</returns>
        [HttpGet("countries")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<Country>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveAllCountries([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _dropdownService.RetrieveAllCountries(pageIndex, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves all languages with pagination.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A paginated list of languages.</returns>
        [HttpGet("languages")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<Language>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveAllLanguages([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var result = await _dropdownService.RetrieveAllLanguages(pageIndex, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves cities of a specific country with pagination.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="country">The country name.</param>
        /// <returns>A paginated list of cities for the specified country.</returns>
        [HttpGet("cities/{country}")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CitiesOfCountry([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromRoute] string country)
        {
            var result = await _dropdownService.CitiesOfCountry(pageIndex, pageSize, country);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves cities of a specific state within a country with pagination.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="country">The country name.</param>
        /// <param name="state">The state name.</param>
        /// <returns>A paginated list of cities for the specified state.</returns>
        [HttpGet("cities/{country}/{state}")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CitiesOfState([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromRoute] string country, [FromRoute] string state)
        {
            var result = await _dropdownService.CitiesOfState(pageIndex, pageSize, country, state);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves states of a specific country with pagination.
        /// </summary>
        /// <param name="pageIndex">The page index (starting from 1).</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <param name="country">The country name.</param>
        /// <returns>A paginated list of states for the specified country.</returns>
        [HttpGet("states/{country}")]
        [ProducesResponseType(typeof(IDataResult<PaginatedList<State>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StatesOfCountry([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromRoute] string country)
        {
            var result = await _dropdownService.StatesOfCountry(pageIndex, pageSize, country);
            return Ok(result);
        }
    }
}
