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
        ArquivoPdf dirPdf;

        public ConversorTxtToPdf(ILogger<ConversorTxtToPdf> logger)
        {
            _logger = logger;
            dirPdf = new();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var diretorios in ArquivoTxt.ColetaDiretorio())
                    {
                        ArquivoTxt arquivoTxt = new();
                        arquivoTxt.LerLinhasDoTxt(diretorios);
                        _logger.LogInformation("Arquivo {titulo} encontrado, Hora: {time}", arquivoTxt.Titulo, DateTimeOffset.Now);

                        dirPdf.GerarPdf(arquivoTxt);
                        _logger.LogInformation("Arquivo {titulo} encontrado, Hora: {time}", arquivoTxt.Titulo, DateTimeOffset.Now);

                        File.Delete(diretorios);
                        _logger.LogInformation("Arquivo {titulo} deletado, Hora: {time}", arquivoTxt.Titulo, DateTimeOffset.Now);
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
    }
}
