//using Infrastructure.Middleware;
//using Common.ConfigurationSettings;
//using Common.DBData;
//using Common.ServiceCollectionExtensions;
//using Common.Services;
//using Common.Swagger;
//using FluentValidation.AspNetCore;
//using FluentValidation;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Persistence.DBContext;
//using Persistence.DBModels;
//using Serilog;
//using Swashbuckle.AspNetCore.Filters;
//using System.Reflection;
//using Common.FluentValidators;

//var builder = WebApplication.CreateBuilder(args);

//// Assign configuration to ConfigurationSettingsHelper
//ConfigurationSettingsHelper.Configuration = builder.Configuration;

//// Configure Serilog
//Log.Logger = new LoggerConfiguration()
//      .ReadFrom.Configuration(builder.Configuration)
//      .Enrich.FromLogContext()
//      .CreateLogger();

//builder.Host.UseSerilog();

//// Register FluentValidation
//builder.Services.AddFluentValidationAutoValidation()
//                .AddFluentValidationClientsideAdapters();
//builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();

//// Register services
//builder.Services.AddHealthTriageServiceLibraryServices();
//builder.Services.AddEndpointsApiExplorer();

//// Configure Swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthTriage", Version = "v1" });
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    if (File.Exists(xmlPath))
//    {
//        c.IncludeXmlComments(xmlPath);
//    }
//    c.ExampleFilters();
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });

//    c.OperationFilter<ErrorOperationFilter>();

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                },
//                Scheme = "oauth2",
//                Name = "Bearer",
//                In = ParameterLocation.Header,
//            },
//            new List<string>()
//        }
//    });
//});

//builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

//var app = builder.Build();

//// Enable CORS
//app.UseCors("AllowAll");

//// Database Migration
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<HealthTriageDbContext>();
//    var connection = dbContext.Database.GetDbConnection();

//    try
//    {
//        using var transaction = await dbContext.Database.BeginTransactionAsync();
//        try
//        {
//            await dbContext.Database.MigrateAsync();
//            await transaction.CommitAsync();
//        }
//        catch (Exception ex)
//        {
//            await transaction.RollbackAsync();
//            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//            logger.LogError(ex, "An error occurred while applying migrations. The changes were rolled back.");
//            throw;
//        }
//    }
//    catch (Exception ex)
//    {
//        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while backing up the database.");
//        throw;
//    }
//    finally
//    {
//        await connection.CloseAsync();
//    }
//}

//// Initialize Roles & Users
//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var context = serviceProvider.GetRequiredService<HealthTriageDbContext>();
//    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
//    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

//    await Initializer.Init(context, roleManager, userManager, app.Environment, builder.Configuration);
//}

//// Middleware and Security
//app.UseSerilogRequestLogging();
//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.UseAuthentication();
//app.UseAuthorization();

//// Swagger UI
//app.UseSwagger();
//app.UseSwaggerUI();

//app.UseStaticFiles();
//app.UseHttpsRedirection();

//// Map Controllers
//app.MapControllers();

//app.Run();

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Infrastructure.Middleware;
using Common.DBData;
using Common.Services;
using Common.Swagger;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Common.FluentValidators;
using FluentValidation;
using Newtonsoft.Json;
using Domain.Interfaces.PractitionerBusiness;
using Infrastructure.Business.Repositories.PractitionerRepo;

var builder = WebApplication.CreateBuilder(args);

// Assign configuration to ConfigurationSettingsHelper
ConfigurationSettingsHelper.Configuration = builder.Configuration;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .Enrich.FromLogContext()
      .CreateLogger();

builder.Host.UseSerilog();

// Load environment-specific settings
var environment = builder.Environment.EnvironmentName; // Gets Development, Production, or Test
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();

// Register services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddTransient<IEmailServiceCustom, EmailService>();
builder.Services.AddTransient<TokenHandler>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ILocalStorage, LocalStorage>();
builder.Services.AddTransient<ILanguageRepository, LanguageRepository>();
builder.Services.AddTransient<IFirebaseStorage, FirebaseStorageService>();
builder.Services.AddTransient<IUserContextManager, UserContextManager>();
builder.Services.AddTransient<IUserManager, AppUserManager>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPractitionerRepository, PractitionerRepository>();
builder.Services.AddTransient<IpractitioerBusiness, practitioerBusiness>();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});
builder.Services.AddHttpClient();
//Database Configuration
//builder.Services.AddDbContext<HealthTriageDbContext>(options =>
//    options.UseSqlServer(ConfigSettings.ConnectionString.DefaultConnection));
builder.Services.AddDbContext<HealthTriageDbContext>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;  // Suppress the default model state invalid filter
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthTriage", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    c.ExampleFilters();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.OperationFilter<ErrorOperationFilter>();

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddIdentity<AppUser, Role>().AddEntityFrameworkStores<HealthTriageDbContext>().AddDefaultTokenProviders()
    .AddTokenProvider<EmailTokenProvider<AppUser>>("email");

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Database Migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HealthTriageDbContext>();
    var connection = dbContext.Database.GetDbConnection();

    try
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync();
        // Drop all tables before migration
//        await dbContext.Database.ExecuteSqlRawAsync(@"
//    DECLARE @sql NVARCHAR(MAX) = N'';

//    -- Step 1: Drop Foreign Key Constraints
//    SELECT @sql += 'ALTER TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) 
//                  + ' DROP CONSTRAINT ' + QUOTENAME(CONSTRAINT_NAME) + ';'
//    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
//    WHERE CONSTRAINT_TYPE = 'FOREIGN KEY';

//    EXEC sp_executesql @sql;

//    -- Step 2: Drop Tables
//    SET @sql = N'';
//    SELECT @sql += 'DROP TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ';'
//    FROM INFORMATION_SCHEMA.TABLES
//    WHERE TABLE_TYPE = 'BASE TABLE';

//    EXEC sp_executesql @sql;
//");

        try
        {
            await dbContext.Database.MigrateAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation(ex, "An error occurred while applying migrations. The changes were rolled back." + JsonConvert.SerializeObject(ex));
            throw;
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation(ex, "An error occurred while backing up the database." + JsonConvert.SerializeObject(ex));
        throw;
    }
    finally
    {
        await connection.CloseAsync();
    }
}

// Initialize Roles & Users
using (var scope = app.Services.CreateScope())
{
    try
    {
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<HealthTriageDbContext>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        await Initializer.Init(context, roleManager, userManager, app.Environment, builder.Configuration);

        // Middleware and Security
        app.UseSerilogRequestLogging();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        // Swagger UI
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        // Map Controllers
        app.MapControllers();

        app.Run();
    }
    catch(Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation(ex, "An error occurred while initializing records to the db." + JsonConvert.SerializeObject(ex));
    }

}


