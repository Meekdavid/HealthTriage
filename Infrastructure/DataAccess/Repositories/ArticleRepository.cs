using Common.Enums;
using Domain.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Persistence.DBContext;
using Persistence.DBModels;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(HealthTriageDbContext HealthTriageDbContext) : base(HealthTriageDbContext)
        {
        }

        public async Task<IQueryable<Article>> GetAllArticles()
        {
            var articles = _ctx.Articles.Where(a => a.Status == Status.Active);

            return articles;
        }

        public async Task<Article> GetArticlesByUserId(string id)
        {
            var article = await _ctx.Articles.Where(a => a.Status == Status.Active && a.UserId == id).FirstOrDefaultAsync();

            return article;
        }

        public async Task<IQueryable<Article>> GetArticlesByUserType(AuthorType userType)
        {
            var articles = _ctx.Articles.Where(a => a.AuthorType == userType && a.Status == Status.Active);

            return articles;
        }

        public async Task<IQueryable<Article>> GetUnapprovedArticles()
        {
            var articles = _ctx.Articles.Where(a => a.ArticleState == ArticleStatus.PendingReview && a.Status == Status.Active);

            return articles;
        }

        public async Task<IQueryable<Article>> SearchForArticles(string searchString)
        {
            searchString = searchString.ToLower();
            var articles = _ctx.Articles.Include(av => av.ArticleViews)
                .Include(ar => ar.ArticleRatings)
                .Include(ac => ac.ArticleComments)
                .Where(a => a.Status == Status.Active &&( a.Title.ToLower().Contains(searchString) 
                || a.Content.ToLower().Contains(searchString) 
                || a.Category.ToString().ToLower().Contains(searchString)));

            return articles;
        }
    }
}
