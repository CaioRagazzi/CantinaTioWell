namespace CantinaTioWell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testecaio : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clientes", "Perfil_Id", "dbo.Perfils");
            DropIndex("dbo.Clientes", new[] { "Perfil_Id" });
            AddColumn("dbo.Clientes", "Perfil", c => c.Int(nullable: false));
            DropColumn("dbo.Clientes", "Perfil_Id");
            DropTable("dbo.Perfils");
        }
        
        public override void Down()
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
            DropColumn("dbo.Clientes", "Perfil");
            CreateIndex("dbo.Clientes", "Perfil_Id");
            AddForeignKey("dbo.Clientes", "Perfil_Id", "dbo.Perfils", "Id");
        }
    }
}
