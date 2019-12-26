using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRM.Services;
using BRM.Services.Interfaces;
using Data.Context;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BRM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MyContext>(options =>
            {
                options.UseMySql(
                    Configuration.GetConnectionString("Storage"));
            }, ServiceLifetime.Transient);

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //services.AddDbContext<MyContext>(opt =>
            //    opt.UseInMemoryDatabase("TodoList"));

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // repository tribe
            //services.AddScoped<IPlacementRepository, PlacementRepository>();
            services.AddScoped<IInterviewHistoryRepository, InterviewHistoryRepository>();
            //services.AddScoped<IEmployeeAssetRepository, EmployeeAssetRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<ISubDistrictRepository, SubDistrictRepository>();
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBatchClassRepository, BatchClassRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IEmployeeAssetRepository, EmployeeAssetRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IInterviewHistoryRepository, InterviewHistoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IParticipantDisplayRepository, ParticipantDisplayRepository>();
            services.AddScoped<IPlacementRepository, PlacementRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IChangePasswordRepository, ChangePasswordRepository>();

            // service tribe
            //services.AddScoped<IPlacementService, PlacementService>();
            services.AddScoped<IInterviewHistoryService, InterviewHistoryService>();
            //services.AddScoped<IEmployeeAssetService, EmployeeAssetService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ISubDistrictService, SubDistrictService>();
            services.AddScoped<IVillageService, VillageService>();
            services.AddScoped<IBatchService, BatchService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBatchClassService, BatchClassService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IEmployeeAssetService, EmployeeAssetService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IParticipantDisplayService, ParticipantDisplayService>();
            services.AddScoped<IPlacementService, PlacementService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IChangePasswordService, ChangePasswordService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
