namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurance_add_Amount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insurances", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Insurances", "Amount");
        }
    }
}
