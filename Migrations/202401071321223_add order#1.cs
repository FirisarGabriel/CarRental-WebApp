namespace Proiect_Rent_A_Car.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorder1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Listing_Id", "dbo.Listings");
            DropIndex("dbo.Orders", new[] { "Listing_Id" });
            RenameColumn(table: "dbo.Orders", name: "Listing_Id", newName: "ListingId");
            AlterColumn("dbo.Orders", "ListingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ListingId");
            AddForeignKey("dbo.Orders", "ListingId", "dbo.Listings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ListingId", "dbo.Listings");
            DropIndex("dbo.Orders", new[] { "ListingId" });
            AlterColumn("dbo.Orders", "ListingId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ListingId", newName: "Listing_Id");
            CreateIndex("dbo.Orders", "Listing_Id");
            AddForeignKey("dbo.Orders", "Listing_Id", "dbo.Listings", "Id");
        }
    }
}
