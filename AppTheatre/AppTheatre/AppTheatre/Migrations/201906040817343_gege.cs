namespace AppTheatre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gege : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Predstavas", "Premijera", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Predstavas", "Premijera", c => c.DateTime(nullable: false));
        }
    }
}
