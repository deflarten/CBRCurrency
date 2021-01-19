using CBRCurrency.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBRCurrency.Data
{
    public interface ICurrencyRepository
    {
        public Task<Currency> GetByCharCodeAsync(string charCode);
        public Task<IEnumerable<Currency>> GetAllAsync();
        public void UpdateRange(IEnumerable<Currency> currencies);
        public Task AddRangeAsync(IEnumerable<Currency> currencies);
        public Task SaveChangesAsync();
    }
}
