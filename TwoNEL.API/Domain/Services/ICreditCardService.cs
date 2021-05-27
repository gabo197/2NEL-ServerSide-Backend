using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCard>> ListAsync();
        Task<CreditCardResponse> GetByIdAsync(int id);
        Task<CreditCardResponse> SaveAsync(CreditCard creditCard);
        Task<CreditCardResponse> UpdateAsync(int id, CreditCard creditCard);
        Task<CreditCardResponse> DeleteAsync(int id);
    }
}
