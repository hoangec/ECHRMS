namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_pro_employeesalarycomponent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeSalaryComponents", "StartApplyDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EmployeeSalaryComponents", "EndApplyDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.EmployeeSalaryComponents", "ApplyDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeSalaryComponents", "ApplyDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EmployeeSalaryComponents", "EndApplyDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.EmployeeSalaryComponents", "StartApplyDate");
        }
    }
}
