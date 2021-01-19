using CBRCurrency.Data;
using CBRCurrency.Models;
using CBRCurrency.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CBRCurrencyWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<CurrencyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CurrencyContext")));
            services.AddSingleton<ICurrencyRepository, CurrencyRepository>();
            services.AddSingleton<ICurrenciesParser, JsonCurrenciesParser>();
            services.AddSingleton<IDownloader, WebClientDownloader>();
            services.AddHostedService<CurrencyInfoUpdateService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
