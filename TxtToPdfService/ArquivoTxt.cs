using System.IO;

namespace TxtToPdfService
{
    public class ArquivoTxt
    {
        public string DiretorioEntrada { get; set; }
        public string Titulo { get; set; }
        public string[] Texto { get; set; }

        public ArquivoTxt(string diretorioEntrada)
        {
            DiretorioEntrada = diretorioEntrada;
        }

        public string[] ColetaDiretorio()
        {
            return Directory.GetFiles(DiretorioEntrada);
        }

        public void LerLinhasDoTxt(string file)
        {
            Texto = File.ReadAllLines(file);

        }

        public bool DeletarArquivoTxt(string titulo)
        {

            return true;
        }
    }
}
