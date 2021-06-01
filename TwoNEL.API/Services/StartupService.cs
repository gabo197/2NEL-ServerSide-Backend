using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Persistence.Repositories;
using TwoNEL.API.Domain.Services;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Services
{
    public class StartupService : IStartupService
    {
        private readonly IStartupRepository startupRepository;
        private readonly IEnterpriseRepository enterpriseRepository;
        private readonly IUnitOfWork unitOfWork;

        public StartupService(IStartupRepository startupRepository, IUnitOfWork unitOfWork, IEnterpriseRepository enterpriseRepository)
        {
            this.startupRepository = startupRepository;
            this.unitOfWork = unitOfWork;
            this.enterpriseRepository = enterpriseRepository;
        }

        public async Task<StartupResponse> DeleteAsync(int enterpriseId, int id)
        {
            var existingEnterprise = await enterpriseRepository.FindById(enterpriseId);
            if (existingEnterprise == null)
                return new StartupResponse("Enterprise not found");
            var existingStartup = await startupRepository.FindById(id);

            if (existingStartup == null)
                return new StartupResponse("Startup not found");

            try
            {
                startupRepository.Remove(existingStartup);
                await unitOfWork.CompleteAsync();

                return new StartupResponse(existingStartup);
            }
            catch (Exception ex)
            {
                return new StartupResponse($"An error ocurred while deleting the startup: {ex.Message}");
            }
        }

        public async Task<StartupResponse> GetByIdAsync(int enterpriseId, int id)
        {
            var existingEnterprise = await enterpriseRepository.FindById(enterpriseId);
            if (existingEnterprise == null)
                return new StartupResponse("Enterprise not found");
            var existingStartup = await startupRepository.FindById(id);

            if (existingStartup == null)
                return new StartupResponse("Startup not found");
            return new StartupResponse(existingStartup);
        }

        public async Task<IEnumerable<Domain.Models.Startup>> ListByEnterpriseIdAsync(int enterpriseId)
        {
            return await startupRepository.ListByEnterpriseIdAsync(enterpriseId);
        }

        public async Task<StartupResponse> SaveAsync(int enterpriseId, Domain.Models.Startup startup)
        {
            var existingEnterprise = await enterpriseRepository.FindById(enterpriseId);
            if (existingEnterprise == null)
                return new StartupResponse("Enterprise not found");
            try
            {
                startup.EnterpriseId = enterpriseId;
                await startupRepository.AddAsync(startup);
                await unitOfWork.CompleteAsync();

                return new StartupResponse(startup);
            }
            catch (Exception ex)
            {
                return new StartupResponse($"An error ocurred while saving the startup: {ex.Message}");
            }
        }

        public async Task<StartupResponse> UpdateAsync(int enterpriseId, int id, Domain.Models.Startup startup)
        {
            var existingEnterprise = await enterpriseRepository.FindById(enterpriseId);
            if (existingEnterprise == null)
                return new StartupResponse("Enterprise not found");
            
            var existingStartup = await startupRepository.FindById(id);

            if (existingStartup == null)
                return new StartupResponse("Startup not found");

            existingStartup.Name = startup.Name;
            existingStartup.Description = startup.Description;

            try
            {
                startupRepository.Update(existingStartup);
                await unitOfWork.CompleteAsync();

                return new StartupResponse(existingStartup);
            }
            catch (Exception ex)
            {
                return new StartupResponse($"An error ocurred while updating the startup: {ex.Message}");
            }
        }
    }
}
