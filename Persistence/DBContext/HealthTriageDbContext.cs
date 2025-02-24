using Common.ConfigurationSettings;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Concrete;
using Persistence.DBModels;
using Persistence.DBModels.JoinDBModels;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBContext
{
    public class HealthTriageDbContext : IdentityDbContext<AppUser, Role, string>, IDataProtectionKeyContext
    {
        public HealthTriageDbContext(DbContextOptions<HealthTriageDbContext> options)
            : base(options){}


        // Override SaveChanges to disable validation

        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        entry.State = EntityState.Detached;
        //    }

        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}

        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        //{
        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        entry.State = EntityState.Detached;
        //    }

        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    var entityName = entityType.ClrType.Name;
            //    var primaryKeyName = $"{entityName}Id";

            //    var property = entityType.FindProperty(primaryKeyName);
            //    if (property != null && property.ClrType == typeof(string))
            //    {
            //        modelBuilder.Entity(entityType.ClrType)
            //                    .Property(primaryKeyName)
            //                    .HasMaxLength(25);
            //    }

            //    foreach (var component in entityType.GetProperties())
            //    {
            //        if (component.ClrType == typeof(DateTime) || component.ClrType == typeof(DateTime?))
            //        {
            //            component.SetValueConverter(new ValueConverter<DateTime, DateTime>(
            //                v => v.ToUniversalTime(),
            //                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            //            ));
            //        }
            //    }
            //}

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (typeof(HealthTriageEntity).IsAssignableFrom(entityType.ClrType))
            //    {
            //        if (entityType.ClrType.BaseType == typeof(Language) || entityType.ClrType.BaseType == typeof(Role) || entityType.ClrType.BaseType == typeof(Country))
            //        {
            //            continue;
            //        }

            //        var parameter = Expression.Parameter(entityType.ClrType, "x");
            //        var property = Expression.Property(parameter, "Status");
            //        var deletedStatus = Expression.Constant(Status.Deleted);
            //        var condition = Expression.NotEqual(property, deletedStatus);

            //        var lambda = Expression.Lambda(condition, parameter);

            //        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            //    }
            //}

            modelBuilder.Entity<SymptomSearchHistorySymptom>()
                .HasKey(ss => new { ss.SymptomSearchHistoryId, ss.SymptomId });

            modelBuilder.Entity<SymptomSearchHistorySymptom>()
                .HasOne(ss => ss.SymptomSearchHistory)
                .WithMany(sh => sh.SymptomSearchHistorySymptoms)
                .HasForeignKey(ss => ss.SymptomSearchHistoryId);

            modelBuilder.Entity<SymptomSearchHistorySymptom>()
                .HasOne(ss => ss.Symptom)
                .WithMany(s => s.SymptomSearchHistorySymptoms)
                .HasForeignKey(ss => ss.SymptomId);

            modelBuilder.Entity<SymptomSearchHistoryTreatmentOption>()
                .HasKey(st => new { st.SymptomSearchHistoryId, st.TreatmentOptionId });

            modelBuilder.Entity<SymptomSearchHistoryTreatmentOption>()
                .HasOne(st => st.SymptomSearchHistory)
                .WithMany(sh => sh.SymptomSearchHistoryTreatmentOptions)
                .HasForeignKey(st => st.SymptomSearchHistoryId);

            modelBuilder.Entity<SymptomSearchHistoryTreatmentOption>()
                .HasOne(st => st.TreatmentOption)
                .WithMany(to => to.SymptomSearchHistoryTreatmentOptions)
                .HasForeignKey(st => st.TreatmentOptionId);

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.AdminId);
                entity.HasOne(e => e.User)
                      .WithOne()
                      .HasForeignKey<Admin>(e => e.UserId);
            });

            modelBuilder.Entity<ArticleComment>(entity =>
            {
                entity.HasKey(e => e.ArticleCommentId);
                entity.HasOne(e => e.Article)
                      .WithMany()
                      .HasForeignKey(e => e.ArticleId);
            });

            modelBuilder.Entity<ArticleRating>(entity =>
            {
                entity.HasKey(e => e.ArticleRatingId);
                entity.HasOne(e => e.Article)
                      .WithMany()
                      .HasForeignKey(e => e.ArticleId);
            });

            modelBuilder.Entity<ArticleView>(entity =>
            {
                entity.HasKey(e => e.ArticleViewId);
                entity.HasOne(e => e.Article)
                      .WithMany()
                      .HasForeignKey(e => e.ArticleId);
            });

            //modelBuilder.Entity<CommentReply>(entity =>
            //{
            //    entity.HasKey(e => e.CommentReplyId);
            //    entity.HasOne(e => e.ArticleComment)
            //          .WithMany(e => e.CommentReplies)
            //          .HasForeignKey(e => e.CommentId)
            //          .OnDelete(DeleteBehavior.NoAction);
            //});

            modelBuilder.Entity<CommentReply>(entity =>
            {
                entity.HasKey(e => e.CommentReplyId);
                entity.HasOne(e => e.ArticleComment)
                      .WithMany(e => e.CommentReplies)
                      .HasForeignKey(e => e.CommentId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<ConsultancyChat>(entity =>
            {
                entity.HasKey(e => e.ConsultancyChatId);
            });

            modelBuilder.Entity<ConsultationHistory>(entity =>
            {
                entity.HasKey(e => e.ConsultationHistoryId);
                entity.HasOne(e => e.AppUser)
                      .WithMany()
                      .HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Practitioner)
                      .WithMany()
                      .HasForeignKey(e => e.PractitionerId);
            });

            modelBuilder.Entity<HealthcareFacility>(entity =>
            {
                entity.HasKey(e => e.HealthcareFacilityId);
            });

            modelBuilder.Entity<MedicalActivityLog>(entity =>
            {
                entity.HasKey(e => e.MedicalActivityLogId);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Practitioner>(entity =>
            {
                entity.HasKey(e => e.PractitionerId);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<PractitionerRating>(entity =>
            {
                entity.HasKey(e => e.RatingId);
                entity.HasOne(e => e.Practitioner)
                      .WithMany()
                      .HasForeignKey(e => e.PractitionerId);
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.HasKey(e => e.SymptomId);
            });

            modelBuilder.Entity<SymptomSearchHistory>(entity =>
            {
                entity.HasKey(e => e.SymptomSearchHistoryId);
            });

            modelBuilder.Entity<TreatmentOption>(entity =>
            {
                entity.HasKey(e => e.TreatmentOptionId);
            });

            modelBuilder.Entity<FAQ>(entity =>
            {
                entity.HasKey(e => e.FAQId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.PhoneCode);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageName);
            });

            //modelBuilder.Entity<Role>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //});

            //modelBuilder.Entity<Role>().ToTable("AspNetRoles");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = ConfigSettings.ConnectionString.DefaultConnection;

            // Define log file path
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Error", "OnBuild", "log.txt");

            // Ensure the directory exists
            string logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Create a delegate-based logging mechanism to avoid locking issues
            optionsBuilder.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information) // Log to Console
                .LogTo(message =>
                {
                    try
                    {
                        // Append text without locking the file
                        using (StreamWriter writer = File.AppendText(logFilePath))
                        {
                            writer.WriteLine(message);
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("Logging error: " + ex.Message);
                    }
                }, LogLevel.Information);

        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleRating> ArticleRatings { get; set; }
        public DbSet<ArticleView> ArticleViews { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<HealthcareFacility> HealthcareFacilities { get; set; }
        public DbSet<Practitioner> Practitioners { get; set; }
        public DbSet<PractitionerRating> PractitionerRatings { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<SymptomSearchHistory> SymptomSearchHistories { get; set; }
        public DbSet<TreatmentOption> TreatmentOptions { get; set; }
        public DbSet<MedicalActivityLog> MedicalActivityLogs { get; set; }
        public DbSet<ConsultationHistory> ConsultationHistories { get; set; }
        public DbSet<ConsultancyChat> ConsultancyChats { get; set; }
    }
}
