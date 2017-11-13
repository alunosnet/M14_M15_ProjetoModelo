using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M14_M15_ProjetoModelo
{
    class Elemento
    {
        public int id { get; set; }
        public string texto { get; set; }
        public override string ToString()
        {
            return texto;
        }
    }
}
