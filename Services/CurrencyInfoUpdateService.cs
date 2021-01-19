using CBRCurrency.Data;
using CBRCurrency.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CBRCurrency.Services
{
    public class CurrencyInfoUpdateService : BackgroundService
    {
        private readonly ICurrencyRepository _repository;
        private readonly ICurrenciesParser _parser;
        private readonly IDownloader _downloader;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CurrencyInfoUpdateService> _logger;

        public CurrencyInfoUpdateService(ICurrencyRepository repository, IDownloader downloader,
                                         ICurrenciesParser parser, IConfiguration configuration, ILogger<CurrencyInfoUpdateService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _downloader = downloader ?? throw new ArgumentNullException(nameof(downloader));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Сервис получения данных о курсах валют выполняет очередной запрос");

                var data = _downloader.DownloadString(_configuration.GetValue<string>("CbrFileUrl"));
                _logger.LogInformation("Данные загружены успешно");

                IEnumerable<Currency> currencies = _parser.Parse(data);
                _logger.LogInformation("Парсинг данных прошел успешно");

                var allCurrencies = await _repository.GetAllAsync();
                _logger.LogInformation("Обращение к репозиторию прошло успешно");

                if (allCurrencies.Any())
                {
                    _logger.LogInformation("Обновление данных в репозитории");
                    _repository.UpdateRange(currencies);
                }
                else
                {
                    _logger.LogInformation("Добавление данных в репозиторий");
                    await _repository.AddRangeAsync(currencies);
                }
                _logger.LogInformation("Сохранение изменений в репозитории");
                await _repository.SaveChangesAsync();
                _logger.LogInformation("Сохранение прошло успешно, сервис отработал успешно");

                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}