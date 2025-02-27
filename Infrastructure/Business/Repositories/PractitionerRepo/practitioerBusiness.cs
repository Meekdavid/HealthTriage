using AutoMapper;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using Persistence.DBModels;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Common.DTOs;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Database;
using Domain.Interfaces.Business;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using Common.Enums;

namespace Infrastructure.Business.Repositories.PractitionerRepo
{
    public class practitioerBusiness : IpractitioerBusiness
    {

        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<practitioerBusiness> _logger;
        protected readonly IUserManager _userRepository;
        private readonly IUserContextManager _userContextService;
        private readonly IUserRepository _userRepo;
        private readonly IPractitionerRepository _practitionerRepo;
        private readonly ILocalStorage _storage;
        private readonly IPractitionerRatingRepository _practitionerRatingRepo;
        private readonly IMedicalActivityRepository _medicalActivityRepo;
        private UserManager<AppUser> _userManager { get; set; }

        public practitioerBusiness(
            UserManager<AppUser> userManager,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<practitioerBusiness> logger,
        IUserManager userRepository,
        IUserContextManager userContextService,
        IUserRepository userRepo,
        IPractitionerRepository practitionerRepo,
        ILocalStorage storage,
        IPractitionerRatingRepository practitionerRatingRepo,
        IMedicalActivityRepository medicalActivityRepo
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userRepository = userRepository;
            _userContextService = userContextService;
            _userRepo = userRepo;
            _practitionerRepo = practitionerRepo;
            _storage = storage;
            _practitionerRatingRepo = practitionerRatingRepo;
            _medicalActivityRepo = medicalActivityRepo;
            _userManager = userManager;
        }
        public async Task<Core.Results.IResult> AddPractitioner(PractitionerRequest request, HttpContext httpContext)
        {
            _logger.LogInformation($"About Saving new Practitioner with Request {JsonConvert.SerializeObject(request)}");

            var existingUser = await _userRepo.GetByIdAsync(request.UserId);

            if (existingUser == null)
            {
                _logger.LogInformation($"User not found for email: {request.UserId}");
                return new ErrorDataResult<string>("Only valid HealthTriage Account Holder can Register", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            var practitionerDto = _mapper.Map<Practitioner>(request);

            string pathNewName = $"Uploads/Practitioner/{practitionerDto.PractitionerId}";
            var certificate = await _storage.SingleUploadAsync(pathNewName, request.ApplicationCertificate, httpContext);
            practitionerDto.ApplicationCertificateUrl = certificate.Data.pathOrContainerName;

            await _practitionerRepo.AddAsync(practitionerDto);

            await _userManager.AddToRoleAsync(existingUser, "Practitioner");
            await _userManager.RemoveFromRoleAsync(existingUser, "Patient");            

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>(practitionerDto.PractitionerId);
        }

        public async Task<Core.Results.IResult> ApprovePractitionerApplication(string id)
        {
            _logger.LogInformation($"About Approving Practitioner Application for Practitioner {id}");

            var existingPractitioner = await _practitionerRepo.GetByIdAsync(id);

            if (existingPractitioner == null)
            {
                _logger.LogInformation($"Practitioner not found for PractitionerId: {id}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            existingPractitioner.Status = Status.Active;

            await _practitionerRepo.Update(existingPractitioner);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Practitioner Approved Successfully");
        }

        public async Task<Core.Results.IResult> DeletePractitioner(string id)
        {
            _logger.LogInformation($"About Deleting Record for Practitioner {id}");

            var existingPractitioner = await _practitionerRepo.GetByIdAsync(id);

            if (existingPractitioner == null)
            {
                _logger.LogInformation($"Practitioner not found for PractitionerId: {id}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            existingPractitioner.Status = Status.Deleted;
            existingPractitioner.DeletedDate = DateTime.UtcNow;

            await _practitionerRepo.Update(existingPractitioner);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Practitioner Deleted Successfully");
        }

        public async Task<Core.Results.IResult> RatePractitioner(int rate, string id)
        {
            _logger.LogInformation($"About Rating Practitioner {id} with rate {rate}");

            var existingPractitioner = await _practitionerRepo.GetByIdAsync(id);

            if (existingPractitioner == null)
            {
                _logger.LogInformation($"Practitioner not found for PractitionerId: {id}");
                return new ErrorDataResult<string>($"Practitioner not found for PractitionerId: {id}", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            string currentUserId = await _userContextService.GetUserId();
            string userRole = (await _userContextService.GetUserRoles()).FirstOrDefault();
            var ratingDTO = new PractitionerRating();
            ratingDTO.UserId = currentUserId;
            ratingDTO.PractitionerId = id;
            ratingDTO.Rating = rate;
            ratingDTO.UserId = currentUserId;

            await _practitionerRatingRepo.AddAsync(ratingDTO);

            var currentPractitionerRatings = await _practitionerRatingRepo.GetAll(x => x.PractitionerId == id);
            int sumOfRatings = currentPractitionerRatings.Sum(r => r.Rating)+ rate;
            double averageRating = sumOfRatings/ (currentPractitionerRatings.Count() + 1);

            existingPractitioner.TotalRating += 1;
            existingPractitioner.Rating = averageRating;

            await _practitionerRepo.Update(existingPractitioner);

            var userType = userRole switch
            {
                "Patient" => AuthorType.Patient,
                "Practitioner" => AuthorType.Practitioner,
                _ => AuthorType.Patient
            };
            var medicalActivity = new MedicalActivityLog();
            medicalActivity.UserId = currentUserId;
            medicalActivity.ActivityType = Common.Enums.ActivityType.RatePractitioner;
            medicalActivity.UserType = userType;
            medicalActivity.Details = $"rated practitioner '{existingPractitioner.PractitionerName}' with {rate} stars";

            await _medicalActivityRepo.AddAsync(medicalActivity);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Practitioner Rated Successfully");
        }

        public async Task<IDataResult<PaginatedList<PractitionerResponseDto>>> RetrieveAllPractitioners(int pageIndex, int pageSize)
        {
            _logger.LogInformation($"About Retrieving Review all Practitioners with pageNumber: {pageIndex} and pageSize {pageSize}");

            var practitioners = await _practitionerRepo.GetAllPractitioners();

            var paginatedResult = await PaginatedList<PractitionerResponseDto>
                .CreateAsync(practitioners
                .ProjectTo<PractitionerResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<PractitionerResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PractitionerResponseDto>> RetrievePractitionerById(string Id)
        {
            _logger.LogInformation($"About Retrieving Practitioner with id {Id}");

            var existingPractitioner = await _practitionerRepo.GetByIdAsync(Id);            

            if (existingPractitioner == null)
            {
                _logger.LogInformation($"Practitioner not found for PractitionerId: {Id}");
                return new ErrorDataResult<PractitionerResponseDto>(null, StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }
            var result = _mapper.Map<PractitionerResponseDto>(existingPractitioner);

            return new SuccessDataResult<PractitionerResponseDto>(result);
        }

        public async Task<IDataResult<PaginatedList<PractitionerResponseDto>>> RetrieveUnapprovedPractitioners(int pageIndex, int pageSize)
        {
            _logger.LogInformation($"About Retrieving Review all Unapproved Practitioners with pageNumber: {pageIndex} and pageSize {pageSize}");

            var practitioners = await _practitionerRepo.GetUnapprovedPractitioners();

            var paginatedResult = await PaginatedList<PractitionerResponseDto>
                .CreateAsync(practitioners
                .ProjectTo<PractitionerResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<PractitionerResponseDto>>(paginatedResult);
        }

        public async Task<IDataResult<PaginatedList<PractitionerResponseDto>>> SearchPractitioners(int pageIndex, int pageSize, string searchString)
        {
            _logger.LogInformation($"About to search for Practitioners with pageNumber: {pageIndex} pageSize {pageSize} and searchString {searchString}");

            var practitioners = await _practitionerRepo.SearchForPractitioners(searchString);

            var paginatedResult = await PaginatedList<PractitionerResponseDto>
                .CreateAsync(practitioners
                .ProjectTo<PractitionerResponseDto>(_mapper.ConfigurationProvider)
                .AsQueryable(), pageIndex, pageSize);

            return new SuccessDataResult<PaginatedList<PractitionerResponseDto>>(paginatedResult);
        }
    }
}
