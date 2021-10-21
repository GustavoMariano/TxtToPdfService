using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TxtToPdfService
{
    public class ConversorTxtToPdf : BackgroundService
    {
        private readonly ILogger<ConversorTxtToPdf> _logger;
        string[] arquivosTxt = null;
        ArquivoTxt arquivoTxt;
        ArquivoPdf escreverPdf;

        public ConversorTxtToPdf(ILogger<ConversorTxtToPdf> logger)
        {
            var config = InitConfiguration();
            _logger = logger;
            arquivoTxt = new ArquivoTxt(config.GetSection("Diretorio").GetSection("Caminhos").GetSection("RepositorioEntrada").Value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    arquivosTxt = arquivoTxt.ColetaDiretorio();

                    foreach (var arquivo in arquivosTxt)
                    {
                        arquivoTxt.LerLinhasDoTxt(arquivo);
                        _logger.LogInformation("Arquivo {} encontrado, Hora: {time}", arquivo, DateTimeOffset.Now);


                        _logger.LogInformation("Arquivo {} encontrado, Hora: {time}", arquivo, DateTimeOffset.Now);

                        File.Delete(arquivo);
                        _logger.LogInformation("Arquivo {} deletado, Hora: {time}", arquivo, DateTimeOffset.Now);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            return config;
        }
    }
}
