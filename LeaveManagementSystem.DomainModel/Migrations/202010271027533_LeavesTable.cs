namespace LeaveManagementSystem.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeavesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NoOfDays = c.Int(nullable: false),
                        LeaveStatus = c.String(),
                        LeaveDescription = c.String(),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveID)
                .ForeignKey("dbo.Employees", t => t.LeaveID)
                .Index(t => t.LeaveID);
            
            AddColumn("dbo.Employees", "LeaveID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "LeaveID", "dbo.Employees");
            DropIndex("dbo.Leaves", new[] { "LeaveID" });
            DropColumn("dbo.Employees", "LeaveID");
            DropTable("dbo.Leaves");
        }
    }
}
