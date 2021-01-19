using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CBRCurrency.Models
{
    public class JsonCurrenciesParser : ICurrenciesParser
    {
        private readonly ILogger<JsonCurrenciesParser> _logger;
        public JsonCurrenciesParser(ILogger<JsonCurrenciesParser> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<Currency> Parse(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                _logger.LogError("В метод Parse передана пустая строка либо null");
                throw new ArgumentNullException(nameof(input));
            }

            var rates = JObject.Parse(input);
            var date = DateTime.Parse(rates["Date"].ToString());
            _logger.LogInformation($"Начинается запись данных за {date.ToLongDateString()}");

            List<Currency> currencies = new List<Currency>();
            foreach (var rate in rates["Valute"])
            {
                var rateData = rate.First;
                var charCode = rateData["CharCode"].ToString();

                _logger.LogInformation($"Ведётся парсинг валюты с кодом {charCode}");

                var name = rateData["Name"].ToString();
                var value = Decimal.Parse(rateData["Value"].ToString());
                var nominal = Int32.Parse(rateData["Nominal"].ToString());

                _logger.LogInformation($"Парсинг {charCode} прошел успешно");
                currencies.Add(new Currency() { CharCode = charCode, DateTime = date, Name = name, Rate = value, Nominal = nominal });
            }
            return currencies;
        }
    }
}
