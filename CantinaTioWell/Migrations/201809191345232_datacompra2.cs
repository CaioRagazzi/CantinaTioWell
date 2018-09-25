namespace CantinaTioWell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datacompra2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "DataCompra", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "DataCompra");
        }
    }
}
