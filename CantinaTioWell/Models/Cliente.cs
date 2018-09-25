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

        [Required(ErrorMessage = "Obrigatório")]
        private string Nome;

        public string nome
        {
            get { return Nome; }
            set { Nome = value; }
        }

        [Required(ErrorMessage = "Obrigatório")]
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

        [Required(ErrorMessage = "Obrigatório")]
        private string Cpf;

        public string cpf
        {
            get { return Cpf; }
            set { Cpf = value; }
        }

        [Required(ErrorMessage = "Obrigatório")]
        private string senha;

        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        [Required(ErrorMessage = "Obrigatório")]
        private int idPerfil;

        public int Perfil
        {
            get { return idPerfil; }
            set { idPerfil = value; }
        }
    }
}