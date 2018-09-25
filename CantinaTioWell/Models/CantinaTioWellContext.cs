using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CantinaTioWell.Models
{
    public class CantinaTioWellContext : DbContext
    {
        

        public CantinaTioWellContext() : base("Cantina")
        {
        }

        public DbSet<Produto> Produtoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        //public DbSet<Perfil> Perfis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CantinaTioWellContext>(new CreateDatabaseIfNotExists<CantinaTioWellContext>());
            base.OnModelCreating(modelBuilder);

        }
    }
}