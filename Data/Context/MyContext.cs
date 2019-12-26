using Data.Models;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;

        // dasboard
        public DbSet<ReportDistributionVM> DistributionVM { get; set; }
        public DbSet<PlanRealizationVM> PlanRealizationVMs { get; set; }
        public DbSet<ReportUniversityTopVM> ReportUniversityTopVMs { get; set; }
        public DbSet<ReportBootcampQuantityVM> reportBootcampQuantityVMs { get; set; }

        //export
        public DbSet<DistributionReportVM> DistributionReportVM { get; set; }
        public DbSet<UnivLocationReportVM> UnivLocationReportVM { get; set; }
        public DbSet<PlanRealizationReportVM> PlanRealizationReportVM { get; set; }
        public DbSet<TopTenReportVM> TopTenReportVM { get; set; }

        // base 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDisplayVM> EmployeeDisplayVMs { get; set; }
        public DbSet<TrainerVM> TrainerVMs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        // placement
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantVM> ParticipantVMs { get; set; }
        public DbSet<ParticipantDisplayVM> ParticipantDisplayVMs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerVM> CustomerVMs { get; set; }
        public DbSet<CustomerFilterVM> CustomerFilterVMs { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<PlacementVM> PlacementVMs { get; set; }

        // Batch Class
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<BatchClass> BatchClasses { get; set; }
        public DbSet<BatchClassDisplayVM> BatchClassDisplayVMs { get; set; }
        public DbSet<ParticipantDisplayVM> participantDisplayVMs { get; set; }

        //InterviewHistory
        public DbSet<InterviewHistory> InterviewHistories { get; set; }
        public DbSet<InterviewHistoryVM> InterviewHistoryVMs { get; set; }
        public DbSet<InsertInterviewHistoryVM> InsertInterviewHistoryVMs { get; set; }

        //EmployeeAsset
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetDisplayVM> AssetDisplayVMs { get; set; }
        public DbSet<AssetCountVM> assetCountVMs { get; set; }
        public DbSet<EmployeeAsset> EmployeeAssets { get; set; }
        public DbSet<EmployeeAssetVM> EmployeeAssetVMs { get; set; }
        public DbSet<Models.Type> Types { get; set; }

        //Region
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Subdistrict> Subdistricts { get; set; }
        public DbSet<Village> Villages { get; set; }

        //LoginbaseRole
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<EmployeeRoleVM> EmployeeRoleVMs { get; set; }

        //UserCredential
        public DbSet<UserCredentialVM> UserCredentialVMs { get; set; }
        public DbSet<GetTokenByEmailVM> GetTokenByEmailVM { get; set; }

        public MyContext() { }

        public MyContext(DbContextOptions<MyContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Storage");
                optionsBuilder.UseMySql(connectionString);
            }
        }
    }
}
