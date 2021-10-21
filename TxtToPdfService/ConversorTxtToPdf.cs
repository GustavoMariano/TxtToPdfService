using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TxtToPdfService
{
    public class ConversorTxtToPdf : BackgroundService
    {
        private readonly ILogger<ConversorTxtToPdf> _logger;
        ArquivoPdf dirPdf;
        Stopwatch stopwatch = new();
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
                    stopwatch.Start();
                    Parallel.ForEach(ArquivoTxt.ColetaDiretorio(), (diretorios) => BodyForeach(diretorios));

                    #region Em Série
                    //foreach (var diretorios in ArquivoTxt.ColetaDiretorio())
                    //{
                    //    ArquivoTxt arquivoTxt = new();                        

                    //    arquivoTxt.LerLinhasDoTxt(diretorios);
                    //    _logger.LogInformation("Arquivo {titulo} encontrado, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);

                    //    dirPdf.GerarPdf(arquivoTxt);
                    //    _logger.LogInformation("Arquivo {titulo} convertido para PDF, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);

                    //    File.Delete(diretorios);
                    //    _logger.LogInformation("Arquivo {titulo} deletado, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);
                    //}
                    #endregion

                    stopwatch.Stop();
                    _logger.LogInformation("TEMPO DECORRIDO: {stopwatch}", stopwatch.ElapsedMilliseconds / 1000.0);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        #region Métodos Privados
        private void BodyForeach(string diretorios)
        {
            ArquivoTxt arquivoTxt = new();

            arquivoTxt.LerLinhasDoTxt(diretorios);
            _logger.LogInformation("Arquivo {titulo} encontrado, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);

            dirPdf.GerarPdf(arquivoTxt);
            _logger.LogInformation("Arquivo {titulo} convertido para PDF, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);

            File.Delete(diretorios);
            _logger.LogInformation("Arquivo {titulo} deletado, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);
        }
        #endregion
    }
}
