namespace LeaveManagementSystem.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "UserImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "UserImg");
        }
    }
}
