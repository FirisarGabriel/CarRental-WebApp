namespace Proiect_Rent_A_Car.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewtables2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Extras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        WarrantyCost = c.Int(nullable: false),
                        Description = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        PickUpDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Agent_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Agent_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Agent_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Extras", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Agent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Listings", "CarId", "dbo.Cars");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "Agent_Id" });
            DropIndex("dbo.Listings", new[] { "CarId" });
            DropIndex("dbo.Extras", new[] { "Order_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.Listings");
            DropTable("dbo.Extras");
        }
    }
}
