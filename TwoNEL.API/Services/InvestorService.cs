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
    public class InvestorService : IInvestorService
    {
        private readonly IInvestorRepository investorRepository;
        private readonly IUnitOfWork unitOfWork;

        public InvestorService(IInvestorRepository investorRepository, IUnitOfWork unitOfWork)
        {
            this.investorRepository = investorRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<InvestorResponse> GetByIdAsync(int id)
        {
            var existingInvestor = await investorRepository.FindById(id);

            if (existingInvestor == null)
                return new InvestorResponse("Investor not found");
            return new InvestorResponse(existingInvestor);
        }

        public async Task<IEnumerable<Investor>> ListAsync()
        {
            return await investorRepository.ListAsync();
        }

        public async Task<InvestorResponse> SaveAsync(Investor investor)
        {
            try
            {
                await investorRepository.AddAsync(investor);
                await unitOfWork.CompleteAsync();

                return new InvestorResponse(investor);
            }
            catch (Exception ex)
            {
                return new InvestorResponse($"An error ocurred while saving the investor: {ex.Message}");
            }
        }

        public async Task<InvestorResponse> UpdateAsync(int id, Investor investor)
        {
            var existingInvestor = await investorRepository.FindById(id);

            if (existingInvestor == null)
                return new InvestorResponse("Investor not found");

            existingInvestor.FirstName = investor.FirstName;
            existingInvestor.LastName = investor.LastName;
            existingInvestor.Portfolio = investor.Portfolio;

            try
            {
                investorRepository.Update(existingInvestor);
                await unitOfWork.CompleteAsync();

                return new InvestorResponse(existingInvestor);
            }
            catch (Exception ex)
            {
                return new InvestorResponse($"An error ocurred while updating the investor: {ex.Message}");
            }
        }
    }
}
