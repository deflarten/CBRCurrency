using CBRCurrency.Models;
using Microsoft.EntityFrameworkCore;

namespace CBRCurrency.Data
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Currency> Currencies { get; set; }
    }
}
