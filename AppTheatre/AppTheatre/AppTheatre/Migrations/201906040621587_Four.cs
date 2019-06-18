namespace AppTheatre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Four : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Glumacs", "PozoristeId", "dbo.Pozoristes");
            DropIndex("dbo.Glumacs", new[] { "PozoristeId" });
            DropColumn("dbo.Glumacs", "PozoristeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Glumacs", "PozoristeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Glumacs", "PozoristeId");
            AddForeignKey("dbo.Glumacs", "PozoristeId", "dbo.Pozoristes", "Id", cascadeDelete: true);
        }
    }
}
