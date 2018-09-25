namespace CantinaTioWell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraId = c.Int(nullable: false, identity: true),
                        Cliente_id = c.Int(),
                        Produto_id = c.Int(),
                    })
                .PrimaryKey(t => t.CompraId)
                .ForeignKey("dbo.Clientes", t => t.Cliente_id)
                .ForeignKey("dbo.Produtoes", t => t.Produto_id)
                .Index(t => t.Cliente_id)
                .Index(t => t.Produto_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compras", "Produto_id", "dbo.Produtoes");
            DropForeignKey("dbo.Compras", "Cliente_id", "dbo.Clientes");
            DropIndex("dbo.Compras", new[] { "Produto_id" });
            DropIndex("dbo.Compras", new[] { "Cliente_id" });
            DropTable("dbo.Compras");
        }
    }
}
