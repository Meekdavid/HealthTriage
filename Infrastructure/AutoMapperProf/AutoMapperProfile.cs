using AutoMapper;
using Common.DTOs;
using Common.Models;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AutoMapperProf
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleResponseDto>()
            .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => src.ArticleViews.Count))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.ArticleRatings.Any() ? src.ArticleRatings.Average(r => r.Rating) : 0))
            .ForMember(dest => dest.TotalComments, opt => opt.MapFrom(src => src.ArticleComments.Count))
            .ForMember(dest => dest.Views, opt => opt.MapFrom(src => src.ArticleViews))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.ArticleRatings))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.ModifiedDate))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.AuthorType, opt => opt.MapFrom(src => src.AuthorType.ToString()))
            .ForMember(dest => dest.ArticleState, opt => opt.MapFrom(src => src.ArticleState.ToString()))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.ArticleComments));

            CreateMap<ArticleView, ArticleViewDto>();

            CreateMap<ArticleRating, ArticleRatingDto>();

            CreateMap<ArticleComment, ArticleCommentDto>()
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.CommentReplies))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<CommentReply, CommentReplyDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<ConsultationHistory, ConsultationHistoryResponseDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.FullName : null))
            .ForMember(dest => dest.PractitionerName, opt => opt.MapFrom(src => src.Practitioner != null ? src.Practitioner.PractitionerName : ""))
            .ForMember(dest => dest.ConsultancyChats, opt => opt.MapFrom(src => src.ConsultancyChats));

            CreateMap<ConsultancyChat, ConsultancyChatResponseDto>();
            CreateMap<FAQ, FAQResponseDto>();
            CreateMap<HealthcareFacility, HealthcareFacilityResponseDto>();
            CreateMap<Language, LanguageResponseDto>();
            CreateMap<MedicalActivityLog, MedicalActivityLogResponseDto>()
            .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType.ToString())); // Converts enum to string
            CreateMap<Practitioner, PractitionerResponseDto>();
            CreateMap<Symptom, SymptomResponseDto>();
            CreateMap<SymptomSearchHistory, SymptomSearchHistoryResponseDto>()
            .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src =>
                src.SymptomSearchHistorySymptoms.Select(s => s.Symptom.Title).ToList()))
            .ForMember(dest => dest.TreatmentOptions, opt => opt.MapFrom(src =>
                src.SymptomSearchHistoryTreatmentOptions.Select(t => t.TreatmentOption.Name).ToList()));

            CreateMap<TreatmentOption, TreatmentOptionResponseDto>()
            .ForMember(dest => dest.TreatmentType, opt => opt.MapFrom(src => src.TreatmentType.ToString())) // Map enum to string
            .ForMember(dest => dest.SeverityLevel, opt => opt.MapFrom(src => src.SeverityLevel.ToString())) // Map enum to string
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<UserRegisterRequest, AppUser>().ReverseMap();
        }
    }
}
