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
        private readonly IUnitOfWork unitOfWork;

        public StartupService(IStartupRepository startupRepository, IUnitOfWork unitOfWork)
        {
            this.startupRepository = startupRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<StartupResponse> DeleteAsync(int id)
        {
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

        public async Task<StartupResponse> GetByIdAsync(int id)
        {
            var existingStartup = await startupRepository.FindById(id);

            if (existingStartup == null)
                return new StartupResponse("Startup not found");
            return new StartupResponse(existingStartup);
        }

        public async Task<IEnumerable<Domain.Models.Startup>> ListAsync()
        {
            return await startupRepository.ListAsync();
        }

        public async Task<StartupResponse> SaveAsync(Domain.Models.Startup startup)
        {
            try
            {
                await startupRepository.AddAsync(startup);
                await unitOfWork.CompleteAsync();

                return new StartupResponse(startup);
            }
            catch (Exception ex)
            {
                return new StartupResponse($"An error ocurred while saving the startup: {ex.Message}");
            }
        }

        public async Task<StartupResponse> UpdateAsync(int id, Domain.Models.Startup startup)
        {
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
