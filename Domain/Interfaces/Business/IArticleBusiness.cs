using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IArticleBusiness
    {
        Task<Core.Results.IResult> AddArticle(ArticleRequest request, HttpContext httpContext);
        Task<Core.Results.IResult> RateArticle(int rate, string Id);
        Task<Core.Results.IResult> DeleteArticle(string id);
        Task<Core.Results.IResult> ApproveArticle(string id);
        Task<IDataResult<PaginatedList<ArticleResponseDto>>> RetrieveAllArticles(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<ArticleResponseDto>>> RetrieveUnpublishedArticles(int pageIndex, int pageSize);
        Task<IDataResult<PaginatedList<ArticleResponseDto>>> SearchArticles(int pageIndex, int pageSize, string searchString);
        Task<IDataResult<ArticleResponseDto>> RetrieveArticleById(string Id);
    }
}
