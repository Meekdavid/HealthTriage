using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface ISymptomSearchHistoryRepository : IGenericRepository<SymptomSearchHistory>
    {
        Task<IQueryable<SymptomSearchHistory>> RetrieveSymptomSearchHistoryForSpecificUser(string userId);
    }
}
