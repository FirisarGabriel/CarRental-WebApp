namespace Proiect_Rent_A_Car.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class junctiontable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderExtras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ExtrasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderOrderExtras",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        OrderExtras_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.OrderExtras_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.OrderExtras", t => t.OrderExtras_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.OrderExtras_Id);
            
            AddColumn("dbo.Extras", "OrderExtras_Id", c => c.Int());
            CreateIndex("dbo.Extras", "OrderExtras_Id");
            AddForeignKey("dbo.Extras", "OrderExtras_Id", "dbo.OrderExtras", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderOrderExtras", "OrderExtras_Id", "dbo.OrderExtras");
            DropForeignKey("dbo.OrderOrderExtras", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Extras", "OrderExtras_Id", "dbo.OrderExtras");
            DropIndex("dbo.OrderOrderExtras", new[] { "OrderExtras_Id" });
            DropIndex("dbo.OrderOrderExtras", new[] { "Order_Id" });
            DropIndex("dbo.Extras", new[] { "OrderExtras_Id" });
            DropColumn("dbo.Extras", "OrderExtras_Id");
            DropTable("dbo.OrderOrderExtras");
            DropTable("dbo.OrderExtras");
        }
    }
}
