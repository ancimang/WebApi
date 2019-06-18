namespace AppTheatre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateOffset : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Predstavas", "DatumOdrazavanja", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Predstavas", "VremeOdrzavanja", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Predstavas", "VremeOdrzavanja", c => c.DateTime());
            AlterColumn("dbo.Predstavas", "DatumOdrazavanja", c => c.DateTime());
        }
    }
}
