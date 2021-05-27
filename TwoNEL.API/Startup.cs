using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Persistence.Contexts;
using TwoNEL.API.Domain.Persistence.Repositories;
using TwoNEL.API.Domain.Services;
using TwoNEL.API.Persistence.Repositories;
using TwoNEL.API.Services;

namespace TwoNEL.API
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

            services.AddControllers();

            // Database Connection Configuration

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Dependency Injection Configuration

            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IStartupRepository, StartupRepository>();
            services.AddScoped<IStartupTagRepository, StartupTagRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileTagRepository, ProfileTagRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IEntrepreneurRepository, EntrepreneurRepository>();
            services.AddScoped<IInvestorRepository, InvestorRepository>();
            services.AddScoped<IFreelancerRepository, FreelancerRepository>();

            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IStartupService, StartupService>();
            services.AddScoped<IStartupTagService, StartupTagService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileTagService, ProfileTagService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IEntrepreneurService, EntrepreneurService>();
            services.AddScoped<IInvestorService, InvestorService>();
            services.AddScoped<IFreelancerService, FreelancerService>();

            // Apply Endpoints Naming Convention
            services.AddRouting(options => options.LowercaseUrls = true);

            // AutoMapper Setup
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TwoNEL.API", Version = "v1" });
                c.EnableAnnotations();
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "API WSVAP (WebSmartView)", Version = "v1" });
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //app.UseSwagger(c =>
                //{
                //    c.RouteTemplate = "tunel/swagger/{documentName}/swagger.json";
                //});
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwoNEL.API v1"));
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json"
                //});
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
