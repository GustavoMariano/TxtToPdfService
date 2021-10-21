using Microsoft.Extensions.Configuration;
using System.IO;

namespace TxtToPdfService
{
    public class ArquivoTxt
    {
        public static string DiretorioEntrada { get; set; }
        public string Titulo { get; set; }
        public string[] Texto { get; set; }

        public ArquivoTxt()
        {
        }

        public static string[] ColetaDiretorio()
        {
            var config = InitConfiguration();
            DiretorioEntrada = config.GetSection("Diretorio").GetSection("Caminhos").GetSection("RepositorioEntrada").Value;
            return Directory.GetFiles(DiretorioEntrada, "*.txt");
        }

        public void LerLinhasDoTxt(string file)
        {
            Titulo = file.Replace(DiretorioEntrada, "");
            Texto = File.ReadAllLines(file);
        }

        #region Métodos Privados
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            return config;
        }
        #endregion
    }
}
