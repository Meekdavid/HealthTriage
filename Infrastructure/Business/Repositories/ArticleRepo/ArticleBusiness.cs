using AutoMapper;
using Common.DTOs;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces;
using Domain.Interfaces.Business;
using Domain.Interfaces.Database;
using Infrastructure.Business.Repositories.PractitionerRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Tls;
using Persistence.DBModels;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Business.Repositories.ArticleRepo
{
    public class ArticleBusiness : IArticleBusiness
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<practitioerBusiness> _logger;
        protected readonly IUserManager _userRepository;
        private readonly IUserContextManager _userContextService;
        private readonly IUserRepository _userRepo;
        private readonly IFAQRepository _faqRepo;
        private readonly ILocalStorage _storage;
        private readonly IPractitionerRatingRepository _practitionerRatingRepo;
        private readonly IMedicalActivityRepository _medicalActivityRepo;
        private readonly IArticleRepository _articleRepo;
        private readonly IArticleRatingRepository _articleRating;

        public ArticleBusiness(IMapper mapper,
           IUnitOfWork unitOfWork,
           ILogger<practitioerBusiness> logger,
           IUserManager userRepository,
           IUserContextManager userContextService,
           IUserRepository userRepo,
           IFAQRepository faqRepo,
           ILocalStorage storage,
           IPractitionerRatingRepository practitionerRatingRepo,
           IMedicalActivityRepository medicalActivityRepo,
           IArticleRepository articleRepo,
           IArticleRatingRepository articleRating)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userRepository = userRepository;
            _userContextService = userContextService;
            _userRepo = userRepo;
            _faqRepo = faqRepo;
            _storage = storage;
            _practitionerRatingRepo = practitionerRatingRepo;
            _medicalActivityRepo = medicalActivityRepo;
            _articleRepo = articleRepo;
            _articleRating = articleRating;
        }

        public async Task<Core.Results.IResult> AddArticle(ArticleRequest request, HttpContext httpContext)
        {
            _logger.LogInformation($"About Saving new Article with Request {JsonConvert.SerializeObject(request)}");

            string pathNewName = $"Uploads/Article/{request.Title}";
            var coverPicture = await _storage.SingleUploadAsync(pathNewName, request.CoverPhoto, httpContext);

            var articleDto = _mapper.Map<Article>(request);
            articleDto.CoverPhotoUrl = coverPicture.Data.pathOrContainerName;

            await _articleRepo.AddAsync(articleDto);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Article Created Successfully and Awaiting Approval");
        }

        public async Task<Core.Results.IResult> ApproveArticle(string id)
        {
            _logger.LogInformation($"About Approving Article {id}");

            var existingArticle = await _articleRepo.GetByIdAsync(id);

            if (existingArticle == null)
            {
                _logger.LogInformation($"Article not found for ArticleId: {id}");
                return new ErrorDataResult<string>("", StatusCode_ArticleNotFound, StatusMessage_ArticleNotFound);
            }

            string currentUserId = await _userContextService.GetUserId();

            existingArticle.ArticleState = Common.Enums.ArticleStatus.Published;
            existingArticle.ApprovedBy = currentUserId;

            await _articleRepo.Update(existingArticle);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Article Approved Successfully");
        }

        public async Task<Core.Results.IResult> DeleteArticle(string id)
        {
            _logger.LogInformation($"About Deleting Article {id}");

            var existingArticle = await _articleRepo.GetByIdAsync(id);

            if (existingArticle == null)
            {
                _logger.LogInformation($"Article not found for ArticleId: {id}");
                return new ErrorDataResult<string>("", StatusCode_ArticleNotFound, StatusMessage_ArticleNotFound);
            }

            string currentUserId = await _userContextService.GetUserId();

            existingArticle.Status = Status.Deleted;
            existingArticle.DeletedDate = DateTime.UtcNow;

            await _articleRepo.Update(existingArticle);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Article Deleted Successfully");
        }

        public async Task<Core.Results.IResult> RateArticle(int rate, string Id)
        {
            _logger.LogInformation($"About Rating Article {Id} with Rate {rate}");

            var existingArticle = await _articleRepo.GetByIdAsync(Id);

            if (existingArticle == null)
            {
                _logger.LogInformation($"Practitioner not found for PractitionerId: {Id}");
                return new ErrorDataResult<string>("", StatusCode_ArticleNotFound, StatusMessage_ArticleNotFound);
            }

            string currentUserId = await _userContextService.GetUserId();

            existingArticle.Status = Status.Deleted;
            existingArticle.DeletedDate = DateTime.UtcNow;

            var newArticleRatingDto = new ArticleRating();
            newArticleRatingDto.ArticleId = existingArticle.ArticleId;
            newArticleRatingDto.UserId = currentUserId;
            newArticleRatingDto.Rating = rate;

            await _articleRating.AddAsync(newArticleRatingDto);

            await _articleRepo.Update(existingArticle);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Article Deleted Successfully");
        }

        public async Task<IDataResult<PaginatedList<ArticleResponseDto>>> RetrieveAllArticles(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<ArticleResponseDto>> RetrieveArticleById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<PaginatedList<ArticleResponseDto>>> RetrieveUnpublishedArticles(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<PaginatedList<ArticleResponseDto>>> SearchArticles(int pageIndex, int pageSize, string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
