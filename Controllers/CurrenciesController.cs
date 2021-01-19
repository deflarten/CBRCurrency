using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CBRCurrency.Data;
using CBRCurrency.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CBRCurrency.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyRepository _repository;
        private readonly ILogger<CurrenciesController> _logger;

        public CurrenciesController(ICurrencyRepository repository, ILogger<CurrenciesController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Currencies
        [HttpGet]
        [Route("Currencies")]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            _logger.LogInformation("Поступил запрос GET api/Currencies");
            var currencies = await _repository.GetAllAsync();
            _logger.LogInformation("Запрос обработан успешно");
            return Ok(currencies);
        }

        // GET: api/Currencies/5
        [HttpGet]
        [Route("Currency/{charCode}")]
        public async Task<ActionResult<Currency>> GetCurrency(string charCode)
        {
            _logger.LogInformation($"Поступил запрос GET api/Currency/{charCode}");
            var currency = await _repository.GetByCharCodeAsync(charCode);

            if (currency == null)
            {
                _logger.LogWarning($"Не удалось найти запрошенный элемент, id (charCode) элемента: {charCode}");
                return NotFound();
            }
            _logger.LogInformation("Запрос обработан успешно");
            return Ok(currency);
        }
    }
}
