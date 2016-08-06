namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_empcode_pro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeCode", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "VoluntaryInsurance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "VoluntaryInsurance", c => c.Double());
            DropColumn("dbo.Employees", "EmployeeCode");
        }
    }
}
