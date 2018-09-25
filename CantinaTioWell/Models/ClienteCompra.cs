using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class ClienteCompra
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
    }
}