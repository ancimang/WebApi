namespace AppTheatre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Predstavas", "Pozoriste_Id", "dbo.Pozoristes");
            DropIndex("dbo.Predstavas", new[] { "Pozoriste_Id" });
            RenameColumn(table: "dbo.Predstavas", name: "Pozoriste_Id", newName: "PozoristeId");
            AlterColumn("dbo.Predstavas", "PozoristeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Predstavas", "PozoristeId");
            AddForeignKey("dbo.Predstavas", "PozoristeId", "dbo.Pozoristes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predstavas", "PozoristeId", "dbo.Pozoristes");
            DropIndex("dbo.Predstavas", new[] { "PozoristeId" });
            AlterColumn("dbo.Predstavas", "PozoristeId", c => c.Int());
            RenameColumn(table: "dbo.Predstavas", name: "PozoristeId", newName: "Pozoriste_Id");
            CreateIndex("dbo.Predstavas", "Pozoriste_Id");
            AddForeignKey("dbo.Predstavas", "Pozoriste_Id", "dbo.Pozoristes", "Id");
        }
    }
}
