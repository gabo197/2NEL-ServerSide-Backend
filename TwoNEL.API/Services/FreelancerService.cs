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
    public class FreelancerService : IFreelancerService
    {
        private readonly IFreelancerRepository freelancerRepository;
        private readonly IUnitOfWork unitOfWork;

        public FreelancerService(IFreelancerRepository freelancerRepository, IUnitOfWork unitOfWork)
        {
            this.freelancerRepository = freelancerRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<FreelancerResponse> GetByIdAsync(int id)
        {
            var existingFreelancer = await freelancerRepository.FindById(id);

            if (existingFreelancer == null)
                return new FreelancerResponse("Freelancer not found");
            return new FreelancerResponse(existingFreelancer);
        }

        public async Task<IEnumerable<Freelancer>> ListAsync()
        {
            return await freelancerRepository.ListAsync();
        }

        public async Task<FreelancerResponse> SaveAsync(Freelancer freelancer)
        {
            try
            {
                await freelancerRepository.AddAsync(freelancer);
                await unitOfWork.CompleteAsync();

                return new FreelancerResponse(freelancer);
            }
            catch (Exception ex)
            {
                return new FreelancerResponse($"An error ocurred while saving the freelancer: {ex.Message}");
            }
        }

        public async Task<FreelancerResponse> UpdateAsync(int id, Freelancer freelancer)
        {
            var existingFreelancer = await freelancerRepository.FindById(id);

            if (existingFreelancer == null)
                return new FreelancerResponse("Freelancer not found");

            existingFreelancer.FirstName = freelancer.FirstName;
            existingFreelancer.LastName = freelancer.LastName;
            existingFreelancer.Portfolio = freelancer.Portfolio;
            existingFreelancer.Specialty = freelancer.Specialty;

            try
            {
                freelancerRepository.Update(existingFreelancer);
                await unitOfWork.CompleteAsync();

                return new FreelancerResponse(existingFreelancer);
            }
            catch (Exception ex)
            {
                return new FreelancerResponse($"An error ocurred while updating the freelancer: {ex.Message}");
            }
        }
    }
}
