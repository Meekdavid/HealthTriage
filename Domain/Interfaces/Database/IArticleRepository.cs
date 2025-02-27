using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Database
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task<IQueryable<Article>> GetAllArticles();
        Task<IQueryable<Article>> GetUnapprovedArticles();
        Task<IQueryable<Article>> SearchForArticles(string searchString);
        Task<IQueryable<Article>> GetArticlesByUserType(AuthorType userType);
        Task<Article> GetArticlesByUserId(string id);
    }
}
