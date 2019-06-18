namespace AppTheatre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Glumacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        GodinaRodjenja = c.Int(),
                        PozoristeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pozoristes", t => t.PozoristeId, cascadeDelete: true)
                .Index(t => t.PozoristeId);
            
            CreateTable(
                "dbo.Vezas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlumacId = c.Int(nullable: false),
                        PredstavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Glumacs", t => t.GlumacId, cascadeDelete: true)
                .ForeignKey("dbo.Predstavas", t => t.PredstavaId, cascadeDelete: true)
                .Index(t => t.GlumacId)
                .Index(t => t.PredstavaId);
            
            CreateTable(
                "dbo.Predstavas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                        ImeRezisera = c.String(nullable: false),
                        Opis = c.String(nullable: false),
                        DatumOdrazavanja = c.DateTime(),
                        VremeOdrzavanja = c.DateTime(),
                        CenaKarte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Premijera = c.DateTime(nullable: false),
                        VrstaPredstaveId = c.Int(nullable: false),
                        Pozoriste_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VrstaPredstaves", t => t.VrstaPredstaveId, cascadeDelete: true)
                .ForeignKey("dbo.Pozoristes", t => t.Pozoriste_Id)
                .Index(t => t.VrstaPredstaveId)
                .Index(t => t.Pozoriste_Id);
            
            CreateTable(
                "dbo.VrstaPredstaves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 15),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pozoristes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 40),
                        UpravnikImePrezime = c.String(maxLength: 30),
                        GodinaOsnivanja = c.Int(nullable: false),

                    Grad = c.String(nullable: false, maxLength: 20),
                        Adresa = c.String(nullable: false, maxLength: 30),
                        Telefon = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Glumacs", "PozoristeId", "dbo.Pozoristes");
            DropForeignKey("dbo.Predstavas", "Pozoriste_Id", "dbo.Pozoristes");
            DropForeignKey("dbo.Predstavas", "VrstaPredstaveId", "dbo.VrstaPredstaves");
            DropForeignKey("dbo.Vezas", "PredstavaId", "dbo.Predstavas");
            DropForeignKey("dbo.Vezas", "GlumacId", "dbo.Glumacs");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Predstavas", new[] { "Pozoriste_Id" });
            DropIndex("dbo.Predstavas", new[] { "VrstaPredstaveId" });
            DropIndex("dbo.Vezas", new[] { "PredstavaId" });
            DropIndex("dbo.Vezas", new[] { "GlumacId" });
            DropIndex("dbo.Glumacs", new[] { "PozoristeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pozoristes");
            DropTable("dbo.VrstaPredstaves");
            DropTable("dbo.Predstavas");
            DropTable("dbo.Vezas");
            DropTable("dbo.Glumacs");
        }
    }
}
