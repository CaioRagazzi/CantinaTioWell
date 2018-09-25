using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class ProdutoCompra
    {
        public string NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int IdProduto { get; set; }
        public int IdCompra { get; set; }
        public DateTime DataCompra { get; set; }
    }
}