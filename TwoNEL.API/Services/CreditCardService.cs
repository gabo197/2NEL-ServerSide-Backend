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
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository creditCardRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreditCardService(ICreditCardRepository creditCardRepository, IUnitOfWork unitOfWork)
        {
            this.creditCardRepository = creditCardRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreditCardResponse> DeleteAsync(int id)
        {
            var existingCreditCard = await creditCardRepository.FindById(id);

            if (existingCreditCard == null)
                return new CreditCardResponse("Credit card not found");

            try
            {
                creditCardRepository.Remove(existingCreditCard);
                await unitOfWork.CompleteAsync();

                return new CreditCardResponse(existingCreditCard);
            }
            catch (Exception ex)
            {
                return new CreditCardResponse($"An error ocurred while deleting the credit card: {ex.Message}");
            }
        }

        public async Task<CreditCardResponse> GetByIdAsync(int id)
        {
            var existingCreditCard = await creditCardRepository.FindById(id);

            if (existingCreditCard == null)
                return new CreditCardResponse("Credit card not found");
            return new CreditCardResponse(existingCreditCard);
        }

        public async Task<IEnumerable<CreditCard>> ListAsync()
        {
            return await creditCardRepository.ListAsync();
        }

        public async Task<CreditCardResponse> SaveAsync(CreditCard creditCard)
        {
            try
            {
                await creditCardRepository.AddAsync(creditCard);
                await unitOfWork.CompleteAsync();

                return new CreditCardResponse(creditCard);
            }
            catch (Exception ex)
            {
                return new CreditCardResponse($"An error ocurred while saving the credit card: {ex.Message}");
            }
        }

        public async Task<CreditCardResponse> UpdateAsync(int id, CreditCard creditCard)
        {
            var existingCreditCard = await creditCardRepository.FindById(id);

            if (existingCreditCard == null)
                return new CreditCardResponse("Credit card not found");

            existingCreditCard.CardNumber = creditCard.CardNumber;
            existingCreditCard.Cvv = creditCard.Cvv;
            existingCreditCard.ExpMonth = creditCard.ExpMonth;
            existingCreditCard.ExpYear = creditCard.ExpYear;

            try
            {
                creditCardRepository.Update(existingCreditCard);
                await unitOfWork.CompleteAsync();

                return new CreditCardResponse(existingCreditCard);
            }
            catch (Exception ex)
            {
                return new CreditCardResponse($"An error ocurred while updating the credit card: {ex.Message}");
            }
        }
    }
}
