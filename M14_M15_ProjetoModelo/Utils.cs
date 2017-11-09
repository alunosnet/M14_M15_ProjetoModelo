using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace m14_trabalho_modelo {
    class Utils {
        static public byte[] ImagemParaVetor(string ficheiro) {
            FileStream fs = new FileStream(ficheiro, FileMode.Open, FileAccess.Read);
            byte[] dados = new byte[fs.Length];
            fs.Read(dados, 0, (int)fs.Length);
            fs.Close();
            return dados;
        }
        static public void VetorParaImagem(byte[] imagem,string ficheiro) {
            FileStream fs = new FileStream(ficheiro, FileMode.Create, FileAccess.Write);
            fs.Write(imagem, 0, imagem.GetUpperBound(0));
            fs.Close();
        }
    }
}
