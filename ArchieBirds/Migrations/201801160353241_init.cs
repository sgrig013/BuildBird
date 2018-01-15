namespace ArchieBirds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpecimenName = c.String(nullable: false, maxLength: 30),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Girth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WingWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Double(nullable: false),
                        Longtitude = c.Double(nullable: false),
                        Wings = c.Int(nullable: false),
                        HandThings = c.Int(nullable: false),
                        Skull = c.Int(nullable: false),
                        Teeth = c.Int(nullable: false),
                        Feet = c.Int(nullable: false),
                        Tail = c.Int(nullable: false),
                        Spine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Archies");
        }
    }
}
