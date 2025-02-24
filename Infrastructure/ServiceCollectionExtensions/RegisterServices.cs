using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.AutoMapperProf;
using Microsoft.AspNetCore.Identity.UI.Services;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.DBContext;
using Common.ConfigurationSettings;
using Persistence.DBModels;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Business.Repositories.Storage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Core.Results;
using static Domain.Literals.StringLiterals;
using Infrastructure.Services;
using Infrastructure.Business.Repositories;
using Infrastructure.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Common.ServiceCollectionExtensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddHealthTriageServiceLibraryServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddTransient<IEmailServiceCustom, EmailService>();
            services.AddTransient<TokenHandler>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILocalStorage, LocalStorage>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IFirebaseStorage, FirebaseStorageService>();
            services.AddTransient<IUserContextManager, UserContextManager>();
            services.AddTransient<IUserManager, AppUserManager>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            //Database Configuration
            services.AddDbContext<HealthTriageDbContext>(options =>
                options.UseSqlServer(ConfigSettings.ConnectionString.DefaultConnection));

            services.AddIdentity<AppUser, Role>()
                .AddEntityFrameworkStores<HealthTriageDbContext>()
                .AddDefaultTokenProviders();

            //services.AddDataProtection()
            //    .PersistKeysToDbContext<HealthTriageDbContext>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;  // Suppress the default model state invalid filter
            });

            services.AddAuthorization();
            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(ConfigSettings.ApplicationSetting.JwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
