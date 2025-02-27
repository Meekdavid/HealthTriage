using Common.DTOs;
using Common.Models;
using Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IDropdownBusiness
    {
        Task<IDataResult<PaginatedList<CountryResponseDTO>>> RetrieveAllCountries(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<LanguageResponseDto>>> RetrieveAllLanguages(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<string>>> CitiesOfCountry(int pageIndex, int pageSize, string country);
        Task<IDataResult<PaginatedList<string>>> CitiesOfState(int pageIndex, int pageSize, string country, string state);
        Task<IDataResult<PaginatedList<State>>> StatesOfCountry(int pageIndex, int pageSize, string country);
    }
}
