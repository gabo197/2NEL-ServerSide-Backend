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
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository enterpriseRepository;
        private readonly IEntrepreneurRepository entrepreneurRepository;
        private readonly IUnitOfWork unitOfWork;

        public EnterpriseService(IEnterpriseRepository enterpriseRepository, IUnitOfWork unitOfWork, IEntrepreneurRepository entrepreneurRepository)
        {
            this.enterpriseRepository = enterpriseRepository;
            this.unitOfWork = unitOfWork;
            this.entrepreneurRepository = entrepreneurRepository;
        }

        public async Task<EnterpriseResponse> DeleteAsync(int id)
        {
            var existingEnterprise = await enterpriseRepository.FindById(id);

            if (existingEnterprise == null)
                return new EnterpriseResponse("Enterprise not found");

            try
            {
                enterpriseRepository.Remove(existingEnterprise);
                await unitOfWork.CompleteAsync();

                return new EnterpriseResponse(existingEnterprise);
            }
            catch (Exception ex)
            {
                return new EnterpriseResponse($"An error ocurred while deleting the enterprise: {ex.Message}");
            }
        }

        public async Task<EnterpriseResponse> GetByIdAsync(int id)
        {
            var existingEnterprise = await enterpriseRepository.FindById(id);

            if (existingEnterprise == null)
                return new EnterpriseResponse("Enterprise not found");
            return new EnterpriseResponse(existingEnterprise);
        }

        public async Task<IEnumerable<Enterprise>> ListAsync()
        {
            return await enterpriseRepository.ListAsync();
        }

        public async Task<EnterpriseResponse> SaveAsync(int userId, Enterprise enterprise)
        {
            var existingEntrepreneur = await entrepreneurRepository.FindById(userId);
            if (existingEntrepreneur == null)
                return new EnterpriseResponse("Entrepreneur not found");
            try
            {
                enterprise.EntrepreneurId = userId;
                await enterpriseRepository.AddAsync(userId, enterprise);
                await unitOfWork.CompleteAsync();

                return new EnterpriseResponse(enterprise);
            }
            catch (Exception ex)
            {
                return new EnterpriseResponse($"An error ocurred while saving the enterprise: {ex.Message}");
            }
        }

        public async Task<EnterpriseResponse> UpdateAsync(int id, Enterprise enterprise)
        {
            var existingEnterprise = await enterpriseRepository.FindById(id);

            if (existingEnterprise == null)
                return new EnterpriseResponse("Enterprise not found");

            existingEnterprise.Name = enterprise.Name;
            existingEnterprise.Description = enterprise.Description;
            existingEnterprise.BusinessEmail = enterprise.BusinessEmail;
            existingEnterprise.CorpNumber = enterprise.CorpNumber;
            existingEnterprise.ImageUrl = enterprise.ImageUrl;

            try
            {
                enterpriseRepository.Update(existingEnterprise);
                await unitOfWork.CompleteAsync();

                return new EnterpriseResponse(existingEnterprise);
            }
            catch (Exception ex)
            {
                return new EnterpriseResponse($"An error ocurred while updating the enterprise: {ex.Message}");
            }
        }
    }
}
