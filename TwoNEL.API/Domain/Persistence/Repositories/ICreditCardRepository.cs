using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;


namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface ICreditCardRepository
    {
        Task<IEnumerable<CreditCard>> ListAsync();
        Task<CreditCard> FindById(int id);
        Task AddAsync(CreditCard creditCard);
        void Update(CreditCard creditCard);
        void Remove(CreditCard creditCard);
    }
}
