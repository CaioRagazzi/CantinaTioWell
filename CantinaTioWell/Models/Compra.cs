using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class Compra
    {
        public int CompraId { get; set; }

        public Produto Produto { get; set; }

        public Cliente Cliente { get; set; }

        public Compra()
        {

        }

        public Compra(Produto produto, Cliente cliente)
        {
            Produto = produto;
            Cliente = cliente;
        }

        private DateTime? dataCompra = null;

        public DateTime DataCompra
        {
            get
            {
                return this.dataCompra.HasValue
                ? this.dataCompra.Value
                : DateTime.Now;
            }
            set { dataCompra = value; }
        }


    }
}