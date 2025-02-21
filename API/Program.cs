using Infrastructure.Middleware;
using Common.ConfigurationSettings;
using Common.DBData;
using Common.ServiceCollectionExtensions;
using Common.Services;
using Common.Swagger;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.DBContext;
using Persistence.DBModels;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using Common.FluentValidators;

var builder = WebApplication.CreateBuilder(args);

// Assign configuration to ConfigurationSettingsHelper
ConfigurationSettingsHelper.Configuration = builder.Configuration;

// Add services to the container.
Log.Logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .Enrich.FromLogContext()
      .CreateLogger();

builder.Host.UseSerilog();

// Add FluentValidation

// Use the new FluentValidation registration method
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

// Automatically register all validators from the assembly
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();
builder.Services.AddHealthTriageServiceLibraryServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthTriage", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) // Check if file exists
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
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();
// Enable CORS
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HealthTriageDbContext>();

    var connection = dbContext.Database.GetDbConnection();

    try
    {

        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Database.MigrateAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while applying migrations. The changes were rolled back.");
            throw;
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while backing up the database.");
        throw;
    }
    finally
    {
        await connection.CloseAsync();
    }
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var context = serviceProvider.GetRequiredService<HealthTriageDbContext>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

    await Initializer.Init(context, roleManager, userManager, app.Environment, builder.Configuration);
}

app.UseSerilogRequestLogging(); // Use Serilog to log HTTP requests
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
