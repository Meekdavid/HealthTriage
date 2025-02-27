using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface IFAQRepository : IGenericRepository<FAQ>
    {
        Task<IQueryable<FAQ>> GetAllFAQs();
        Task<IQueryable<FAQ>> SearchForFAQs(string searchString);
    }
}
