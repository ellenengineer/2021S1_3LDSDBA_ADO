using System;


namespace TesteAPIBanco
{
    public partial class Produto
    {
        public Produto()
        {
        }

        public int CodProd { get; set; }
        public int CodTipoProd { get; set; }

         public string NomeProd { get; set; }
        public int QtdEstqProd { get; set; }
        public decimal ValUnitProd { get; set; }
        public decimal? ValTotal { get; set; }

    }
}
