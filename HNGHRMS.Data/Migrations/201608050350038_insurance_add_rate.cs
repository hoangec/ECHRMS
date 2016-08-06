namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurance_add_rate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insurances", "CompanyRatePercent", c => c.Double(nullable: false));
            AddColumn("dbo.Insurances", "LabaratorRatePercent", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Insurances", "LabaratorRatePercent");
            DropColumn("dbo.Insurances", "CompanyRatePercent");
        }
    }
}
