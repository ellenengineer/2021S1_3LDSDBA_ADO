using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Cliente
    {
        public int Cod_Cli { get; set; }
        public TipoCliente TipoCli { get; set; }
        public string Nome_Cli { get; set; }
        public DateTime Data_CadCli { get; set; }
        public double Renda_Cli { get; set; }

        public string Sexo_Cli { get; set; }
    }
}
