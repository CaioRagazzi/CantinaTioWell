using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class Cliente
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

        private string Email;

        public string email
        {
            get { return Email; }
            set { Email = value; }
        }


        private string Telefone;

        public string telefone
        {
            get { return Telefone; }
            set { Telefone = value; }
        }

        private string Cpf;

        public string cpf
        {
            get { return Cpf; }
            set { Cpf = value; }
        }

        private string senha;

        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        private int idPerfil;

        public int Perfil
        {
            get { return idPerfil; }
            set { idPerfil = value; }
        }
    }
}