using AutoMapper;
using Common.Models;
using Common.Pagination;
using Core.Results;
using Domain.Interfaces;
using Domain.Interfaces.PractitionerBusiness;
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

        public practitioerBusiness(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<practitioerBusiness> logger,
        IUserManager userRepository,
        IUserContextManager userContextService,
        IUserRepository userRepo,
        IPractitionerRepository practitionerRepo,
        ILocalStorage storage,
        IPractitionerRatingRepository practitionerRatingRepo
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
        }
        public async Task<Core.Results.IResult> AddPractitioner(PractitionerRequest request, HttpContext httpContext)
        {
            _logger.LogInformation($"About Saving new Practitioner with Request {JsonConvert.SerializeObject(request)}");

            var existingUser = await _userRepo.GetByIdAsync(request.UserId);

            if (existingUser == null)
            {
                _logger.LogInformation($"User not found for email: {request.UserId}");
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            var practitionerDto = _mapper.Map<Practitioner>(request);

            string pathNewName = $"Uploads/Practitioner/{practitionerDto.PractitionerId}";
            var certificate = await _storage.SingleUploadAsync(pathNewName, request.ApplicationCertificate, httpContext);
            practitionerDto.ApplicationCertificateUrl = certificate.Data.pathOrContainerName;

            await _practitionerRepo.AddAsync(practitionerDto);

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
                return new ErrorDataResult<string>("", StatusCode_UserNotFound, StatusMessage_UserNotFound);
            }

            string currentUserId = await _userContextService.GetUserId();
            var ratingDTO = new PractitionerRating();
            ratingDTO.UserId = currentUserId;
            ratingDTO.PractitionerId = id;
            ratingDTO.Rating = rate;
            ratingDTO.UserId = currentUserId;

            await _practitionerRatingRepo.AddAsync(ratingDTO);

            await _practitionerRepo.Update(existingPractitioner);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<string>("Practitioner Deleted Successfully");
        }

        public async Task<IDataResult<PaginatedList<Persistence.DBModels.Practitioner>>> RetrieveAllPractitioners(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<Persistence.DBModels.Practitioner>> RetrievePractitionerById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<PaginatedList<Persistence.DBModels.Practitioner>>> RetrieveUnapprovedPractitioners(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<PaginatedList<Persistence.DBModels.Practitioner>>> SearchPractitioners(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
