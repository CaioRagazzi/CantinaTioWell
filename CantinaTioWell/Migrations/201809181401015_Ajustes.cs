namespace CantinaTioWell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajustes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "Senha", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clientes", "Senha");
        }
    }
}
