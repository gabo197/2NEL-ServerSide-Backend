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
    public class EntrepreneurService : IEntrepreneurService
    {
        private readonly IEntrepreneurRepository entrepreneurRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public EntrepreneurService(IEntrepreneurRepository entrepreneurRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.entrepreneurRepository = entrepreneurRepository;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public async Task<EntrepreneurResponse> GetByIdAsync(int id)
        {
            var existingEntrepreneur = await entrepreneurRepository.FindById(id);

            if (existingEntrepreneur == null)
                return new EntrepreneurResponse("Entrepreneur not found");
            return new EntrepreneurResponse(existingEntrepreneur);
        }

        public async Task<IEnumerable<Entrepreneur>> ListAsync()
        {
            return await entrepreneurRepository.ListAsync();
        }

        public async Task<EntrepreneurResponse> SaveAsync(int userId, Entrepreneur entrepreneur)
        {
            var existingUser = await userRepository.FindById(userId);
            if (existingUser == null)
                return new EntrepreneurResponse("User not found");
            try
            {
                entrepreneur.UserId = userId;
                await entrepreneurRepository.AddAsync(entrepreneur);
                await unitOfWork.CompleteAsync();

                return new EntrepreneurResponse(entrepreneur);
            }
            catch (Exception ex)
            {
                return new EntrepreneurResponse($"An error ocurred while saving the entrepreneur: {ex.Message}");
            }
        }

        public async Task<EntrepreneurResponse> UpdateAsync(int id, Entrepreneur entrepreneur)
        {
            var existingEntrepreneur = await entrepreneurRepository.FindById(id);

            if (existingEntrepreneur == null)
                return new EntrepreneurResponse("User not found");

            existingEntrepreneur.FirstName = entrepreneur.FirstName;
            existingEntrepreneur.LastName = entrepreneur.LastName;
            existingEntrepreneur.Portfolio = entrepreneur.Portfolio;

            try
            {
                entrepreneurRepository.Update(existingEntrepreneur);
                await unitOfWork.CompleteAsync();

                return new EntrepreneurResponse(existingEntrepreneur);
            }
            catch (Exception ex)
            {
                return new EntrepreneurResponse($"An error ocurred while updating the entrepreneur: {ex.Message}");
            }
        }
    }
}
