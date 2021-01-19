using System.Collections.Generic;

namespace CBRCurrency.Models
{
    public interface ICurrenciesParser
    {
        public IEnumerable<Currency> Parse(string input);
    }
}
