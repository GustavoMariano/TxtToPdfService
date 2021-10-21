using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace TxtToPdfService
{
    public class ArquivoPdf : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }

        public void GerarPdf(object texto)
        {

        }
    }
}