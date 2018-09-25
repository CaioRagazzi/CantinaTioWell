using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class Produto
    {
        private int Id;

        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string Nome;

        public string nome
        {
            get { return Nome; }
            set { Nome = value; }
        }

        private decimal Preco;

        public decimal preco
        {
            get { return Preco; }
            set { Preco = value; }
        }
    }
}