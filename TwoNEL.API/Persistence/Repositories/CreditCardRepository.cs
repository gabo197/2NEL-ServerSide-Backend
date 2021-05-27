using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Persistence.Contexts;
using TwoNEL.API.Domain.Persistence.Repositories;

namespace TwoNEL.API.Persistence.Repositories
{
    public class CreditCardRepository : BaseRepository, ICreditCardRepository
    {
        public CreditCardRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(CreditCard creditCard)
        {
            await _context.CreditCards.AddAsync(creditCard);
        }

        public async Task<CreditCard> FindById(int id)
        {
            return await _context.CreditCards.FindAsync(id);
        }

        public async Task<IEnumerable<CreditCard>> ListAsync()
        {
            return await _context.CreditCards.ToListAsync();
        }

        public void Remove(CreditCard creditCard)
        {
            _context.CreditCards.Remove(creditCard);
        }

        public void Update(CreditCard creditCard)
        {
            _context.CreditCards.Update(creditCard);
        }
    }
}
