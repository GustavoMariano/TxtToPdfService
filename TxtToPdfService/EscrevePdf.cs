using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TxtToPdfService
{
    public class EscrevePdf : BackgroundService
    {
        private readonly ILogger<LeitorTxt> _logger;
        ArquivoPdf dirPdf;
        Stopwatch stopwatch = new();
        public EscrevePdf(ILogger<LeitorTxt> logger)
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
                    Parallel.ForEach(Program.ArquivosTxtProcessados, (arquivoTxt) => dirPdf.GerarPdf(arquivoTxt));
                    
                    #region Em Série
                    //foreach (ArquivoTxt arquivoTxt in Program.ArquivosTxtProcessados)
                    //{
                    //    //ArquivoTxt arquivoTxt = new();

                    //    //arquivoTxt.LerLinhasDoTxt(diretorios);
                    //    //_logger.LogInformation("Arquivo {titulo} encontrado, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);

                    //    dirPdf.GerarPdf(arquivoTxt);
                    //    _logger.LogInformation("Arquivo {titulo} convertido para PDF, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);

                    //    Program.ArquivosTxtProcessados.
                    //    Program.ArquivosTxtProcessados.TryDequeue(out arquivoTxt);
                    //    //File.Delete(diretorios);
                    //    //_logger.LogInformation("Arquivo {titulo} deletado, Hora: {time}, Tempo Decorrido: {stopWatch}", arquivoTxt.Titulo, DateTimeOffset.Now, stopwatch.ElapsedMilliseconds / 1000.0);
                    //}
                    #endregion

                    stopwatch.Stop();
                    _logger.LogInformation("TEMPO DECORRIDO: {stopwatch}", stopwatch.ElapsedMilliseconds / 1000.0);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                stopwatch.Stop();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}