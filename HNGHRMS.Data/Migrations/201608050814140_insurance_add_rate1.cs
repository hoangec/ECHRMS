namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurance_add_rate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insurances", "CompanyValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Insurances", "CompanyValue");
        }
    }
}
