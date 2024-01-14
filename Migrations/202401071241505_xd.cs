namespace Proiect_Rent_A_Car.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "Agent_Id" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropColumn("dbo.Orders", "AgentId");
            DropColumn("dbo.Orders", "UserId");
            RenameColumn(table: "dbo.Orders", name: "Agent_Id", newName: "AgentId");
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "AgentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "UserId");
            CreateIndex("dbo.Orders", "AgentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "AgentId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "AgentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Orders", name: "AgentId", newName: "Agent_Id");
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "AgentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "User_Id");
            CreateIndex("dbo.Orders", "Agent_Id");
        }
    }
}
