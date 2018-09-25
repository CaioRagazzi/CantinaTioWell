using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class Perfil
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}