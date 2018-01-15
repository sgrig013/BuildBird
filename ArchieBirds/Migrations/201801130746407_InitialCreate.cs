namespace ArchieBirds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SpecimenName = c.String(maxLength: 30),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Girth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WingWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Double(nullable: false),
                        Longtitude = c.Double(nullable: false),
                        Wings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HandThings = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Skull = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Teeth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Feet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Spine = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Archies");
        }
    }
}
