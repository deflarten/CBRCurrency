using CBRCurrency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBRCurrency.Data
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyContext _context;

        public CurrencyRepository(IServiceScopeFactory scopeFactory)
        {
            _context = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CurrencyContext>();
        }

        public async Task AddRangeAsync(IEnumerable<Currency> currencies)
        {
            await _context.AddRangeAsync(currencies);
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency> GetByCharCodeAsync(string charCode)
        {
            return await _context.FindAsync<Currency>(charCode);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<Currency> currencies)
        {
            _context.UpdateRange(currencies);
        }
    }
}
