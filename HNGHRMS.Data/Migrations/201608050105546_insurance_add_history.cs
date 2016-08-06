namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurance_add_history : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insurances", "IsHistory", c => c.Boolean(nullable: false));
            AddColumn("dbo.Insurances", "HistoryCompanyName", c => c.String());
            AddColumn("dbo.Insurances", "HistoryPositionName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Insurances", "HistoryPositionName");
            DropColumn("dbo.Insurances", "HistoryCompanyName");
            DropColumn("dbo.Insurances", "IsHistory");
        }
    }
}
