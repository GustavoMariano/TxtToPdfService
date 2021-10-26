using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
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

        internal void GerarPdf(ArquivoTxt arquivoTxt)
        {            
            Document doc = new();
            PdfWriter.GetInstance(doc, new FileStream("pdf\\" + arquivoTxt.Titulo.Replace(".txt", ".pdf"), FileMode.Create));

            doc.Open();
            foreach (var linha in arquivoTxt.Texto)
                doc.Add(new Paragraph(linha));
            doc.Close();

            if (!Program.ArquivosTxtProcessados.TryDequeue(out arquivoTxt))
                throw new Exception("Dequeue error");
        }
    }
}