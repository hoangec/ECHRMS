namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_pro_salary_compoenet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeSalaryComponents", "IsMainSalary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeSalaryComponents", "IsMainSalary");
        }
    }
}
