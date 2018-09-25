namespace CantinaTioWell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Perfil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clientes", "Perfil_Id", c => c.Int());
            CreateIndex("dbo.Clientes", "Perfil_Id");
            AddForeignKey("dbo.Clientes", "Perfil_Id", "dbo.Perfils", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clientes", "Perfil_Id", "dbo.Perfils");
            DropIndex("dbo.Clientes", new[] { "Perfil_Id" });
            DropColumn("dbo.Clientes", "Perfil_Id");
            DropTable("dbo.Perfils");
        }
    }
}
